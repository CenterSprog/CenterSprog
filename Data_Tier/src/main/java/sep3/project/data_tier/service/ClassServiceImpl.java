package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.ClassEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.mappers.ClassMapper;
import sep3.project.data_tier.mappers.ParticipantMapper;
import sep3.project.data_tier.repository.IClassRepository;
import sep3.project.data_tier.repository.ILessonRepository;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@GrpcService
public class ClassServiceImpl extends ClassEntityServiceGrpc.ClassEntityServiceImplBase
{
  private IClassRepository classRepository;
  private IUserRepository userRepository;
  private ClassMapper classMapper = ClassMapper.INSTANCE;
  private ParticipantMapper participantMapper = ParticipantMapper.INSTANCE;

  private final static Logger LOG = LoggerFactory.getLogger(ClassServiceImpl.class);

  @Autowired
  public ClassServiceImpl(IClassRepository classRepository, IUserRepository userRepository)
  {
    this.classRepository = classRepository;
    this.userRepository = userRepository;
  }

  @Override
  public void getClassEntityById(RequestGetClassEntity request, StreamObserver<ResponseGetClassEntity> response)
  {
    try {
      String id = request.getClassId();
      Optional<ClassEntity> existingClass = classRepository.findById(id);

      if (existingClass.isEmpty())
      {
        throw new IllegalStateException("No existing class with id " + id);
      }

      ClassData grpcClass = ClassData.newBuilder()
          .setId(existingClass.get().getId())
          .setTitle(existingClass.get().getTitle())
          .setRoom(existingClass.get().getRoom())
          .build();


      response.onNext(ResponseGetClassEntity.newBuilder().setClassEntity(grpcClass).build());
      response.onCompleted();

    }
    catch (Exception e)

    {
      response.onError(
          new Throwable(e.getMessage())
      );
    }
  }

  @Override
  public void getClassEntities(RequestGetClassEntities request, StreamObserver<ResponseGetClassEntities> response)
  {
    try {
      String username = request.getUsername();
      List<ClassEntity> classes = new ArrayList<>();
      System.out.println("Get all classes for : " + username + ", is null " + (username == null ) );
      if(username.equals("") )
        classes = classRepository.findAll();
      else
        classes = classRepository.findByUsers_Username(username);

      for (ClassEntity klasa: classes) {
        System.out.println(klasa.getTitle());
      }

      List<ClassData> grpcsClasses = new ArrayList<>();
      for (ClassEntity entity : classes)
      {
        ClassData grpcClass = ClassData.newBuilder()
            .setId(entity.getId())
            .setTitle(entity.getTitle())
            .setRoom(entity.getRoom()).buildPartial();


        for(UserEntity userEntity : entity.getUsers())
          grpcClass.toBuilder().addParticipants(
              participantMapper.toProto(userEntity)
          );


        grpcsClasses.add(grpcClass);
      }
      ResponseGetClassEntities responseMessage = ResponseGetClassEntities.newBuilder()
          .addAllClassEntities(grpcsClasses)
          .build();

      response.onNext(responseMessage);
      response.onCompleted();
    }
    catch (Exception e)
    {
      response.onError(
          Status.INTERNAL.withDescription("Error fetching classes: " + e.getMessage()).asRuntimeException());
    }
  }


  @Override
  public void createClassEntity(RequestCreateClassEntity request, StreamObserver<ResponseCreateClassEntity> response){
    String title = request.getClassEntityCreation().getTitle();
    String room = request.getClassEntityCreation().getRoom();

    try{

      ClassEntity createdClass = new ClassEntity(title,room);
      classRepository.save(createdClass);

      response.onNext(
              ResponseCreateClassEntity.newBuilder()
                      .setClassEntity(
                              classMapper.toProto(createdClass)
                      ).build()
      );

      response.onCompleted();

    }catch(Exception e){
      response.onError(
              Status.INTERNAL.withDescription("Error creating class : " + e.getMessage()).asRuntimeException()
      );
    }
  }
}
