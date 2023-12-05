package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.HandInHomeworkEntity;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.repository.IHandInHomeworkRepository;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@GrpcService
public class HandInHomeworkServiceImpl extends HandInHomeworkServiceGrpc.HandInHomeworkServiceImplBase
{
  private IHandInHomeworkRepository handInHomeworkRepository;
  private IUserRepository userRepository;
  private IHomeworkRepository homeworkRepository;
  private final static Logger LOG = LoggerFactory.getLogger(ClassServiceImpl.class);

  @Autowired
  public HandInHomeworkServiceImpl(
      IHandInHomeworkRepository handInHomeworkRepository,
      IUserRepository userRepository, IHomeworkRepository homeworkRepository)
  {
    this.handInHomeworkRepository = handInHomeworkRepository;
    this.userRepository = userRepository;
    this.homeworkRepository = homeworkRepository;
  }

  @Override
  public void handInHomework(RequestCreateHandInHomework request, StreamObserver<ResponseGetHandInHomework> response)
  {
    String homeworkId = request.getHomeworkId();
    String studentUsername = request.getStudentUsername();
    HandInHomework grpcs = request.getHandInHomework();

    try
    {
      HandInHomeworkEntity handInHomework = new HandInHomeworkEntity();
      handInHomework.setId(grpcs.getId());
      handInHomework.setAnswer(grpcs.getAnswer());

      UserEntity user = userRepository.findById(studentUsername).orElse(null);
      HomeworkEntity homework = homeworkRepository.findById(homeworkId).orElse(null);

      handInHomework.setUser(user);
      handInHomework.setHomework(homework);

      HandInHomeworkEntity savedHandInHomework = handInHomeworkRepository.save(handInHomework);

      String savedStudentUsername = savedHandInHomework.getUser().getUsername();


      ResponseGetHandInHomework responseGetHandInHomework = ResponseGetHandInHomework.newBuilder()
          .setHandInHomework(HandInHomework.newBuilder()
              .setId(savedHandInHomework.getId())
              .setAnswer(savedHandInHomework.getAnswer())
              .setStudentUsername(savedStudentUsername)
              .build())
          .build();

      response.onNext(responseGetHandInHomework);
      response.onCompleted();

    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "Error handling HandInHomework: " + e.getMessage()).asRuntimeException());
    }
  }
  @Override
  public void getHandInsByHomeworkId(RequestGetHandInsByHomeworkId request, StreamObserver<ResponseGetHandInsByHomeworkId> response)
  {
    String homeworkId = request.getHomeworkId();

    try {
      List<HandInHomeworkEntity> handIns = handInHomeworkRepository.findByHomeworkId(homeworkId);
      List<HandInHomework> handInMessages = new ArrayList<>();

      for (HandInHomeworkEntity entity : handIns) {

        UserEntity user = entity.getUser();
        String studentUsername = user.getUsername();

        HandInHomework message = HandInHomework.newBuilder()
            .setId(entity.getId())
            .setAnswer(entity.getAnswer())
            .setStudentUsername(studentUsername)
            .build();
        handInMessages.add(message);
      }

      ResponseGetHandInsByHomeworkId responseGetHandInsByHomeworkId = ResponseGetHandInsByHomeworkId.newBuilder()
          .addAllHandIns(handInMessages)
          .build();

      response.onNext(responseGetHandInsByHomeworkId);
      response.onCompleted();
    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "Error getting Hand Ins by HomeworkId: " + e.getMessage()).asRuntimeException());
    }
  }
}
