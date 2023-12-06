package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.mappers.UserMapper;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.List;
import java.util.Optional;
import java.util.Random;

@GrpcService
public class UserServiceImpl extends UserServiceGrpc.UserServiceImplBase {
    private UserMapper userMapper = UserMapper.INSTANCE;
    private IUserRepository userRepository;

    @Autowired
    public UserServiceImpl(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public static String generateRandomPassword(int len) {
        String chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghi"
                +"jklmnopqrstuvwxyz!@#$%&";
        Random rnd = new Random();
        StringBuilder sb = new StringBuilder(len);
        for (int i = 0; i < len; i++)
            sb.append(chars.charAt(rnd.nextInt(chars.length())));
        return sb.toString();
    }

    @Override
    public void createUser(RequestCreateUser request, StreamObserver<ResponseCreateUser> response){
        String username = request.getUser().getFirstName() + generateRandomPassword(2);
        String password = generateRandomPassword(8);

        UserEntity newUser = new UserEntity(
                username,
                password,
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

    @Override
    public void getAllUsers(com.google.protobuf.Empty request, StreamObserver<ResponseUserGetAllUsers> response){
        List<UserEntity> users = userRepository.findAll();

        System.out.println("Number of users: " + users.size());
        ResponseUserGetAllUsers responseData = ResponseUserGetAllUsers.newBuilder().buildPartial();
        for(UserEntity user : users){
            if(!user.getRole().equals("admin"))
                responseData = responseData.toBuilder().addUsers(
                        userMapper.toProto(user)
                ).buildPartial();
        }

        response.onNext( responseData.toBuilder().build() );
        response.onCompleted();
    }
}
