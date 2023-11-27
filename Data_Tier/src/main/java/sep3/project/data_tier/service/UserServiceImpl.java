package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.Optional;

@GrpcService
public class UserServiceImpl extends UserServiceGrpc.UserServiceImplBase {
    private IUserRepository userRepository;

    @Autowired
    public UserServiceImpl(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

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
                      UserData.newBuilder()
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


        System.out.println(username);
        Optional<UserEntity> existingUser = userRepository.getByUsername(username);

        if(existingUser.isEmpty())
            response.onNext(
                    ResponseUserGetByUsername.newBuilder()
                            .setUser(
                                UserData.newBuilder().getDefaultInstanceForType().toBuilder().build()
                            )
                            .build()
            );
        else
            response.onNext(
                    ResponseUserGetByUsername.newBuilder().setUser(
                            UserData.newBuilder()
                                    .setUsername(existingUser.get().getUsername())
                                    .setPassword(existingUser.get().getPassword())
                                    .setEmail(existingUser.get().getEmail())
                                    .setFirstName(existingUser.get().getFirstName())
                                    .setLastName(existingUser.get().getLastName())
                                    .setRole(existingUser.get().getRole()).build()
                    )  .build()
            );
        response.onCompleted();
    }


}
