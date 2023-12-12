package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.mappers.HomeworkMapper;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.ILessonRepository;
import sep3.project.protobuf.*;

import java.util.NoSuchElementException;
import java.util.Optional;

@GrpcService public class HomeworkServiceImpl
    extends HomeworkServiceGrpc.HomeworkServiceImplBase
{

  private HomeworkMapper homeworkMapper = HomeworkMapper.INSTANCE;
  private ILessonRepository lessonRepository;
  private IHomeworkRepository homeworkRepository;

  @Autowired public HomeworkServiceImpl(IHomeworkRepository homeworkRepository,
      ILessonRepository lessonRepository)
  {
    this.homeworkRepository = homeworkRepository;
    this.lessonRepository = lessonRepository;
  }

  @Override public void addHomework(RequestAddHomework request,
      StreamObserver<Homework> response)
  {
    String lessonId = request.getLessonId();
    HomeworkEntity homework = new HomeworkEntity(
        request.getHomework().getDeadline(), request.getHomework().getTitle(),
        request.getHomework().getDescription());

    try
    {

      Optional<LessonEntity> existingLesson = lessonRepository.findById(
          lessonId);
      if (existingLesson.isEmpty())
        throw new NoSuchElementException("No existing lesson with id " + lessonId);

      homeworkRepository.save(homework);
      existingLesson.get().setHomework(homework);
      lessonRepository.save(existingLesson.get());

      response.onNext(homeworkMapper.toProto(homework).toBuilder().build());
      response.onCompleted();
    }
    catch (Exception e)
    {
      Status status = Status.INTERNAL.withDescription(e.getMessage());
      response.onError(status.asRuntimeException());
    }
  }

}
