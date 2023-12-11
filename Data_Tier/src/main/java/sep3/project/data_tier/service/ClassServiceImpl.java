package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.hibernate.Hibernate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import sep3.project.data_tier.entity.ClassEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.mappers.ClassMapper;
import sep3.project.data_tier.mappers.LessonMapper;
import sep3.project.data_tier.mappers.UserMapper;
import sep3.project.data_tier.repository.IClassRepository;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.*;

@GrpcService public class ClassServiceImpl
    extends ClassEntityServiceGrpc.ClassEntityServiceImplBase
{
  private IClassRepository classRepository;
  private IUserRepository userRepository;
  private ClassMapper classMapper = ClassMapper.INSTANCE;
  private UserMapper userMapper = UserMapper.INSTANCE;
  private LessonMapper lessonMapper = LessonMapper.INSTANCE;

  @Autowired public ClassServiceImpl(IClassRepository classRepository,
      IUserRepository userRepository)
  {
    this.classRepository = classRepository;
    this.userRepository = userRepository;
  }

  @Override @Transactional public void getClassEntityById(
      RequestGetClassEntity request,
      StreamObserver<ResponseGetClassEntity> response)
  {
    try
    {
      String id = request.getClassId();
      Optional<ClassEntity> existingClass = classRepository.findById(id);

      if (existingClass.isEmpty())
      {
        throw new IllegalStateException("No existing class with id " + id);
      }
      Hibernate.initialize(existingClass.get());

      ClassData grpcClass = ClassData.newBuilder()
          .setId(existingClass.get().getId())
          .setTitle(existingClass.get().getTitle())
          .setRoom(existingClass.get().getRoom()).buildPartial();

      if (!existingClass.get().getLessons().isEmpty())
        for (LessonEntity lessonEntity : existingClass.get().getLessons())
          grpcClass = grpcClass.toBuilder()
              .addLessons(lessonMapper.toOverviewProto(lessonEntity))
              .buildPartial();

      if (!existingClass.get().getUsers().isEmpty())
        for (UserEntity userEntity : existingClass.get().getUsers())
          grpcClass = grpcClass.toBuilder()
              .addParticipants(userMapper.toParticipantProto(userEntity))
              .buildPartial();

      grpcClass.toBuilder().build();

      response.onNext(
          ResponseGetClassEntity.newBuilder().setClassEntity(grpcClass)
              .build());
      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(new Throwable(e.getMessage()));
    }
  }

  @Override public void getClassEntities(RequestGetClassEntities request,
      StreamObserver<ResponseGetClassEntities> response)
  {
    try
    {
      String username = request.getUsername();
      List<ClassEntity> classes;
      System.out.println(
          "Get all classes for : " + username + ", is null " + (username
              == null));
      if (username.equals(""))
        classes = classRepository.findAll();
      else
        classes = classRepository.findByUsers_Username(username);

      for (ClassEntity klasa : classes)
      {
        System.out.println(klasa.getTitle());
      }

      List<ClassData> grpcsClasses = new ArrayList<>();
      for (ClassEntity entity : classes)
      {
        ClassData grpcClass = ClassData.newBuilder().setId(entity.getId())
            .setTitle(entity.getTitle()).setRoom(entity.getRoom())
            .buildPartial();

        System.out.println(entity.getUsers().size());

        for (UserEntity userEntity : entity.getUsers())
          grpcClass = grpcClass.toBuilder()
              .addParticipants(userMapper.toParticipantProto(userEntity))
              .buildPartial();

        grpcClass.toBuilder().build();
        grpcsClasses.add(grpcClass);
      }
      ResponseGetClassEntities responseMessage = ResponseGetClassEntities.newBuilder()
          .addAllClassEntities(grpcsClasses).build();

      response.onNext(responseMessage);
      response.onCompleted();
    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "Error fetching classes: " + e.getMessage()).asRuntimeException());
    }
  }

  @Override @Transactional public void getClassParticipants(
      RequestGetClassParticipants request,
      StreamObserver<ResponseGetClassParticipants> response)
  {
    try
    {
      String id = request.getClassId();
      Optional<ClassEntity> existingClass = classRepository.findById(id);

      if (existingClass.isEmpty())
      {
        throw new IllegalStateException("No existing class with id " + id);
      }

      Hibernate.initialize(existingClass);

      List<UserParticipant> participants = new ArrayList<>();
      for (UserEntity entity : existingClass.get().getUsers())
      {
        if (request.hasRole() && !entity.getRole().equals(request.getRole()))
        {
          continue;
        }

        UserParticipant grpcParticipant = UserParticipant.newBuilder()
            .setFirstName(entity.getFirstName())
            .setLastName(entity.getLastName()).setUsername(entity.getUsername())
            .setEmail(entity.getEmail()).setRole(entity.getRole()).build();

        participants.add(grpcParticipant);
      }

      response.onNext(ResponseGetClassParticipants.newBuilder()
          .addAllParticipants(participants).build());
      response.onCompleted();
    }
    catch (Exception e)

    {
      response.onError(new Throwable(e.getMessage()));
      response.onCompleted();
    }
  }

  @Override public void createClassEntity(RequestCreateClassEntity request,
      StreamObserver<ResponseCreateClassEntity> response)
  {
    String title = request.getClassEntityCreation().getTitle();
    String room = request.getClassEntityCreation().getRoom();

    try
    {

      ClassEntity createdClass = new ClassEntity(title, room);
      classRepository.save(createdClass);

      response.onNext(ResponseCreateClassEntity.newBuilder()
          .setClassEntity(classMapper.toProto(createdClass)).build());

      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "Error creating class : " + e.getMessage()).asRuntimeException());
    }
  }

  @Override @Transactional public void getClassAttendance(
      RequestGetClassAttendance request,
      StreamObserver<ResponseGetClassAttendance> response)
  {
    try
    {
      String id = request.getClassId();
      Optional<ClassEntity> existingClass = classRepository.findById(id);

      if (existingClass.isEmpty())
        throw new IllegalStateException("No existing class with id " + id);

      Hibernate.initialize(existingClass);

      List<LessonAttendance> lessonsAttendance = new ArrayList<>();
      if (!existingClass.get().getLessons().isEmpty())
      {
        for (LessonEntity lessonEntity : existingClass.get().getLessons())
        {
          Set<UserEntity> usersInAttendance = lessonEntity.getAttendance();
          for (UserEntity userEntity : usersInAttendance)
          {
            LessonAttendance lessonAttendance = LessonAttendance.newBuilder()
                .setId(lessonEntity.getId())
                .addParticipants(userMapper.toParticipantProto(userEntity))
                .build();
            lessonsAttendance.add(lessonAttendance);
          }
        }
      }

      response.onNext(ResponseGetClassAttendance.newBuilder()
          .addAllLessonsAttendance(lessonsAttendance).build());
      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(new Throwable(e.getMessage()));
    }
  }

  @Override @Transactional public void getClassAttendanceByUsername(
      RequestGetClassAttendanceByUsername request,
      StreamObserver<ResponseGetClassAttendanceByUsername> response)
  {
    try
    {
      String id = request.getClassId();
      Optional<ClassEntity> existingClass = classRepository.findById(id);

      if (existingClass.isEmpty())
        throw new IllegalStateException("No existing class with id " + id);

      Hibernate.initialize(existingClass);

      List<LessonAttended> lessonsAttended = new ArrayList<>();
      Set<LessonEntity> lessons = existingClass.get().getLessons();
      if (!lessons.isEmpty())
      {
        for (LessonEntity lessonEntity : lessons)
        {
          if (lessonEntity.getAttendance().stream().anyMatch(
              userEntity -> userEntity.getUsername()
                  .equals(request.getUsername())))
          {
            lessonsAttended.add(lessonMapper.toAttendandedProto(lessonEntity));
          }
        }
      }

      response.onNext(ResponseGetClassAttendanceByUsername.newBuilder()
          .addAllLessons(lessonsAttended).build());
      response.onCompleted();

    }
    catch (Exception e)

    {
      response.onError(new Throwable(e.getMessage()));
    }

  }

  @Override public void updateParticipants(
      RequestUpdateClassParticipants request,
      StreamObserver<ResponseUpdateClassParticipants> response)
  {
    String classId = request.getId();
    List<String> participantsUsernames = request.getParticipantsUsernamesList();

    try
    {

      handleAssigningUsersToClass(classId, participantsUsernames);

      response.onNext(
          ResponseUpdateClassParticipants.newBuilder().setResult(true).build());
      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(
          new Throwable(e.getMessage() + " : " + e.getStackTrace()));
    }

  }

  private void handleAssigningUsersToClass(String classId,
      List<String> participantsUsernames)
  {
    ArrayList<UserEntity> newParticipants = new ArrayList<>();
    Optional<ClassEntity> existingClass = classRepository.findById(classId);
    if (existingClass.isEmpty())
      throw new IllegalStateException(
          "Class with given id doesnt exist, cannot update.");
    ClassEntity updatableClass = existingClass.get();

    for (String username : participantsUsernames)
    {
      Optional<UserEntity> existingUser = userRepository.getByUsername(
          username);
      if (existingUser.isPresent())
      {

        // --- Makes sure that the student will always be the part of only one class
        if (existingUser.get().getRole().equals("student"))
          removeUserFromAllClasses(existingUser.get());

        newParticipants.add(existingUser.get());
      }
    }

    updatableClass.setUsers(newParticipants);
    classRepository.save(updatableClass);

  }

  private void removeUserFromAllClasses(UserEntity userEntity)
  {
    List<ClassEntity> classEntities = classRepository.findByUsersContains(
        userEntity);
    for (ClassEntity course : classEntities)
    {
      course.removeUser(userEntity.getUsername());
      classRepository.save(course);
    }
  }
}


