package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.ILessonRepository;
import sep3.project.protobuf.*;

import java.util.Optional;

@GrpcService
public class LessonServiceImpl extends LessonServiceGrpc.LessonServiceImplBase {

    private ILessonRepository lessonRepository;
    private IHomeworkRepository homeworkRepository;

    @Autowired
    public LessonServiceImpl(IHomeworkRepository homeworkRepository, ILessonRepository lessonRepository) {
        this.homeworkRepository = homeworkRepository;
        this.lessonRepository = lessonRepository;
    }

    @Override
    public void getLessonById(RequestGetLessonById request, StreamObserver<ResponseGetLessonById> response){
        try{
            String id = request.getLessonId();
            Optional<LessonEntity> existingLesson = lessonRepository.findById(id);
            if(existingLesson.isEmpty())
                throw new IllegalStateException("No exisitng lesson with id of: " + id);

            Lesson grpcLesson = Lesson.newBuilder()
                    .setDate(existingLesson.get().getDate())
                    .setDescription(existingLesson.get().getDescription())
                    .setTopic(existingLesson.get().getTopic()).buildPartial();

            if(existingLesson.get().getHomework() != null)
                grpcLesson = grpcLesson.toBuilder().setHomework(
                        Homework.newBuilder()
                                .setTitle(existingLesson.get().getHomework().getTitle())
                                .setDeadline(existingLesson.get().getHomework().getDeadline())
                                .setId(existingLesson.get().getHomework().getId())
                                .setDescription(existingLesson.get().getHomework().getDescription())
                                .build()
                ).build();

            response.onNext(
                    ResponseGetLessonById.newBuilder()
                            .setLesson(
                                    grpcLesson
                            ).build()
            );

            response.onCompleted();

        }catch (Exception e){
            response.onError(
                    new Throwable(e.getMessage())
            );
            response.onCompleted();
        }

    }
}
