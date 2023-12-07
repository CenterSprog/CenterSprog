package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.FeedbackEntity;
import sep3.project.data_tier.entity.HandInHomeworkEntity;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.data_tier.mappers.FeedbackMapper;
import sep3.project.data_tier.repository.IFeedbackRepository;
import sep3.project.data_tier.repository.IHandInHomeworkRepository;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.IUserRepository;
import sep3.project.protobuf.*;

import java.util.Optional;

@GrpcService public class FeedbackServiceImpl
    extends FeedbackServiceGrpc.FeedbackServiceImplBase
{
  private IFeedbackRepository feedbackRepository;
  private IHomeworkRepository homeworkRepository;
  private IUserRepository userRepository;
  private IHandInHomeworkRepository handInHomeworkRepository;
  private FeedbackMapper feedbackMapper = FeedbackMapper.INSTANCE;
  private final static Logger LOG = LoggerFactory.getLogger(
      ClassServiceImpl.class);

  @Autowired public FeedbackServiceImpl(IFeedbackRepository feedbackRepository,
      IHomeworkRepository homeworkRepository, IUserRepository userRepository,
      IHandInHomeworkRepository handInHomeworkRepository)
  {
    this.feedbackRepository = feedbackRepository;
    this.homeworkRepository = homeworkRepository;
    this.userRepository = userRepository;
    this.handInHomeworkRepository = handInHomeworkRepository;
  }

  @Override public void addFeedback(RequestAddFeedback request,
      StreamObserver<Feedback> response)
  {
    String handInId = request.getHandInId();
    String studentUsername = request.getStudentUsername();
    FeedbackEntity feedback = new FeedbackEntity(request.getFeedback().getId(),
        request.getFeedback().getGrade(), request.getFeedback().getComment());
    try
    {
      Optional<HandInHomeworkEntity> optionalHandInHomework = handInHomeworkRepository.findByIdAndUser_Username(
          handInId, studentUsername);

      if (optionalHandInHomework.isPresent())
      {
        HandInHomeworkEntity handInHomework = optionalHandInHomework.get();

        if (handInHomework.getFeedback() != null)
        {
          response.onError(Status.ALREADY_EXISTS.withDescription(
                  "Feedback was already provided for this hand-in.")
              .asRuntimeException());
          return;
        }

        FeedbackEntity savedFeedback = feedbackRepository.save(feedback);
        handInHomework.setFeedback(savedFeedback);
        handInHomeworkRepository.save(handInHomework);

        response.onNext(feedbackMapper.toProto(feedback).toBuilder().build());
        response.onCompleted();
      }
      else
      {
        throw new IllegalArgumentException(
            "Hand in homework not found for the given IDs.");
      }
    }
    catch (IllegalArgumentException e)
    {
      response.onError(Status.INVALID_ARGUMENT.withDescription(e.getMessage())
          .asRuntimeException());
    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "An error occurred: " + e.getMessage()).asRuntimeException());
    }
  }

  @Override public void getFeedbackByHandInIdAndStudentUsername(
      RequestGetFeedbackByHandInIdAndStudentUsername request,
      StreamObserver<ResponseGetFeedback> response)
  {
    try
    {
      String handInId = request.getHandInId();
      String studentUsername = request.getStudentUsername();

      Optional<HandInHomeworkEntity> optionalHandInHomework = handInHomeworkRepository.findByIdAndUser_Username(
          handInId, studentUsername);

      if (optionalHandInHomework.isPresent())
      {
        HandInHomeworkEntity handInHomework = optionalHandInHomework.get();
        FeedbackEntity feedback = handInHomework.getFeedback();

        ResponseGetFeedback responseMessage = ResponseGetFeedback.newBuilder()
            .setFeedback(feedbackMapper.toProto(feedback)).build();

        response.onNext(responseMessage);
        response.onCompleted();
      }
      else
      {

        response.onError(Status.NOT_FOUND.withDescription(
                "Hand-in homework not found for the given IDs.")
            .asRuntimeException());
      }
    }
    catch (Exception e)
    {
      response.onError(Status.INTERNAL.withDescription(
          "An error occurred: " + e.getMessage()).asRuntimeException());
    }
  }
}