package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.hibernate.Hibernate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.mappers.HomeworkMapper;
import sep3.project.data_tier.repository.IClassRepository;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.ILessonRepository;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.stream.Collectors;

@GrpcService public class LessonServiceImpl
    extends LessonServiceGrpc.LessonServiceImplBase
{

  private ILessonRepository lessonRepository;
  private IUserRepository userRepository;

  private HomeworkMapper homeworkMapper = HomeworkMapper.INSTANCE;

  @Autowired public LessonServiceImpl(ILessonRepository lessonRepository,
      IUserRepository userRepository)
  {

    this.lessonRepository = lessonRepository;
    this.userRepository = userRepository;
  }

  @Override @Transactional public void getLessonById(
      RequestGetLessonById request,
      StreamObserver<ResponseGetLessonById> response)
  {
    try
    {
      String id = request.getLessonId();
      Optional<LessonEntity> existingLesson = lessonRepository.findById(id);
      if (existingLesson.isEmpty())
        throw new IllegalStateException("No existing lesson with id of: " + id);

      Hibernate.initialize(existingLesson.get());

      LessonData grpcLesson = LessonData.newBuilder()
          .setId(existingLesson.get().getId())
          .setDate(existingLesson.get().getDate())
          .setDescription(existingLesson.get().getDescription())
          .setTopic(existingLesson.get().getTopic()).buildPartial();

      if (existingLesson.get().getHomework() != null)
        grpcLesson = grpcLesson.toBuilder().setHomework(
                homeworkMapper.toProto(existingLesson.get().getHomework()))
            .buildPartial();

      grpcLesson.toBuilder().build();

      response.onNext(
          ResponseGetLessonById.newBuilder().setLesson(grpcLesson).build());

      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(new Throwable(e.getMessage()));
      response.onCompleted();
    }

  }

  @Override @Transactional public void getAttendance(
      RequestGetAttendance request,
      StreamObserver<ResponseGetAttendance> response)
  {
    try
    {
      String id = request.getLessonId();
      Optional<LessonEntity> existingLesson = lessonRepository.findById(id);
      if (existingLesson.isEmpty())
        throw new IllegalStateException("No existing lesson with id of: " + id);

      Hibernate.initialize(existingLesson.get());
      List<UserParticipant> grpcUsers = new ArrayList<>();
      if (!existingLesson.get().getAttendance().isEmpty())
        for (UserEntity userEntity : existingLesson.get().getAttendance())
        {
          UserParticipant grpcUser = UserParticipant.newBuilder()
              .setUsername(userEntity.getUsername())
              .setFirstName(userEntity.getFirstName())
              .setLastName(userEntity.getLastName()).build();
          grpcUsers.add(grpcUser);
        }

      response.onNext(
          ResponseGetAttendance.newBuilder().addAllParticipants(grpcUsers)
              .build());
      response.onCompleted();
    }
    catch (Exception e)
    {
      response.onError(new Throwable(e.getMessage()));
      response.onCompleted();
    }

  }

  @Override public void markAttendance(RequestMarkAttendance request,
      StreamObserver<ResponseMarkAttendance> response)
  {
    String id = request.getLessonId();
    try
    {
      Optional<LessonEntity> existingLesson = lessonRepository.findById(id);

      if (existingLesson.isEmpty())
      {
        throw new IllegalStateException("No existing lesson with id of: " + id);
      }

      List<String> studentUsernames = request.getUsernamesList();
      Set<UserEntity> students = userRepository.findAll().stream()
          .filter(user -> studentUsernames.contains(user.getUsername()))
          .collect(Collectors.toSet());

      existingLesson.get().setAttendance(students);
      lessonRepository.save(existingLesson.get());

      response.onNext(ResponseMarkAttendance.newBuilder()
          .setAmountOfParticipants(students.size()).build());
      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(new Throwable("An error occurred: " + e.getMessage()));

    }
  }

  public void deleteLesson(RequestDeleteLesson request,
      StreamObserver<ResponseDeleteLesson> response)
  {
    try
    {
      String lessonId = request.getLessonId();
      Optional<LessonEntity> lesson = lessonRepository.findById(lessonId);
      if (lesson.isEmpty())
        throw new IllegalStateException(
            "No exisitng lesson with id of: " + lessonId);

      lessonRepository.deleteById(lessonId);

      response.onNext(ResponseDeleteLesson.newBuilder()
          .setStatus(ResponseDeleteLesson.Status.OK)
          .setMessage("Lesson deleted successfully").build());
      response.onCompleted();
    }
    catch (Exception e)
    {
      response.onError(new StatusRuntimeException(
          Status.INTERNAL.withDescription(
              "Error deleting lesson: " + e.getMessage())));
      response.onCompleted();
    }
  }

}


