package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

@GrpcService
public class UserServiceImpl extends UserServiceGrpc.UserServiceImplBase {
    private IUserRepository userRepository;

    @Autowired
    public UserServiceImpl(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

//    NOT YET IMPLEMENTED
//    rpc GetAllUsers(google.protobuf.Empty) returns (ResponseUserGetAllUsers);
//    rpc DeleteUser(RequestDeleteUser) returns (ResponseDeleteUser);

    @Override
    public void createUser(RequestCreateUser request, StreamObserver<ResponseCreateUser> response){
        UserEntity newUser = new UserEntity(
                request.getUser().getUsername(),
                request.getUser().getPassword(),
                request.getUser().getFirstName(),
                request.getUser().getLastName(),
                request.getUser().getEmail(),
                request.getUser().getRole()
        );

        userRepository.save(newUser);
        response.onNext(
              ResponseCreateUser.newBuilder().setUser(
                      sep3.project.protobuf.UserEntity.newBuilder()
                              .setUsername(newUser.getUsername())
                              .setPassword(newUser.getPassword())
                              .setEmail(newUser.getEmail())
                              .setFirstName(newUser.getFirstName())
                              .setLastName(newUser.getLastName())
                              .setRole(newUser.getRole()).build()
              )  .build()
        );
        response.onCompleted();
    }

    @Override
    public void getUserByUsername(RequestUserGetByUsername request, StreamObserver<ResponseUserGetByUsername> response){
        String username = request.getUsername();
        UserEntity existingUser = userRepository.getReferenceById(username);

        response.onNext(
                ResponseUserGetByUsername.newBuilder().setUser(
                        sep3.project.protobuf.UserEntity.newBuilder()
                                .setUsername(existingUser.getUsername())
                                .setPassword(existingUser.getPassword())
                                .setEmail(existingUser.getEmail())
                                .setFirstName(existingUser.getFirstName())
                                .setLastName(existingUser.getLastName())
                                .setRole(existingUser.getRole()).build()
                )  .build()
        );
        response.onCompleted();
    }


}
