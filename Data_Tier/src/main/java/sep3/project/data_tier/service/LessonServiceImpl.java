package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.ClassEntity;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.repository.IClassRepository;
import sep3.project.data_tier.repository.IHomeworkRepository;
import sep3.project.data_tier.repository.ILessonRepository;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@GrpcService
public class LessonServiceImpl extends LessonServiceGrpc.LessonServiceImplBase {

    private ILessonRepository lessonRepository;
    private IHomeworkRepository homeworkRepository;
    private IClassRepository classRepository;

    @Autowired
    public LessonServiceImpl(IHomeworkRepository homeworkRepository, ILessonRepository lessonRepository, IClassRepository classRepository) {
        this.homeworkRepository = homeworkRepository;
        this.lessonRepository = lessonRepository;
        this.classRepository = classRepository;
    }

    @Override
    public void getLessonById(RequestGetLessonById request, StreamObserver<ResponseGetLessonById> response){
        try{
            String id = request.getLessonId();
            Optional<LessonEntity> existingLesson = lessonRepository.findById(id);
            if(existingLesson.isEmpty())
                throw new IllegalStateException("No exisitng lesson with id of: " + id);

            LessonData grpcLesson = LessonData.newBuilder()
                    .setId(existingLesson.get().getId())
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
    @Override
    public void getLessonsByClassId(RequestGetLessonsByClassId request, StreamObserver<ResponseGetLessonsByClassId> response)
    {
        try
        {
            String classId = request.getClassId();

            List<LessonEntity> lessons = lessonRepository.findByClassId(classId);
            List<LessonData> grpcLessons = new ArrayList<>();


            for (LessonEntity lessonEntity : lessons)
            {
                LessonData lesson = LessonData.newBuilder()
                    .setId(lessonEntity.getId())
                    .setDate(lessonEntity.getDate())
                    .setTopic(lessonEntity.getTopic())
                    .setDescription(lessonEntity.getDescription()).buildPartial();

                if (lessonEntity.getHomework() != null)
                {
                    LessonData.Builder Lesson = lesson.toBuilder().setHomework(
                        Homework.newBuilder()
                            .setId(lessonEntity.getHomework().getId())
                            .setTitle(lessonEntity.getHomework().getTitle())
                            .setDeadline(lessonEntity.getHomework().getDeadline())
                            .setId(lessonEntity.getHomework().getId())
                            .setDescription(lessonEntity.getHomework().getDescription())
                            .build());
                }
                grpcLessons.add(lesson);
            }
            response.onNext(ResponseGetLessonsByClassId.newBuilder().addAllLessons(grpcLessons).build());
            response.onCompleted();
        }
        catch (Exception e){
            response.onError(new Throwable(e.getMessage()));
            response.onCompleted();
        }
    }
}
