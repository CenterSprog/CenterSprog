package sep3.project.data_tier.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import sep3.project.data_tier.entity.ClassEntity;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.entity.UserEntity;
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

@GrpcService
public class LessonServiceImpl extends LessonServiceGrpc.LessonServiceImplBase {

    private ILessonRepository lessonRepository;
    private IUserRepository userRepository;
    private IHomeworkRepository homeworkRepository;
    private IClassRepository classRepository;


    @Autowired
    public LessonServiceImpl(IHomeworkRepository homeworkRepository, ILessonRepository lessonRepository, IClassRepository classRepository, IUserRepository userRepository) {
        this.homeworkRepository = homeworkRepository;
        this.lessonRepository = lessonRepository;
        this.classRepository = classRepository;
        this.userRepository = userRepository;
    }

    @Override
    public void getLessonById(RequestGetLessonById request, StreamObserver<ResponseGetLessonById> response) {
        try {
            String id = request.getLessonId();
            Optional<LessonEntity> existingLesson = lessonRepository.findById(id);
            if (existingLesson.isEmpty())
                throw new IllegalStateException("No exisitng lesson with id of: " + id);

            LessonData grpcLesson = LessonData.newBuilder()
                    .setId(existingLesson.get().getId())
                    .setDate(existingLesson.get().getDate())
                    .setDescription(existingLesson.get().getDescription())
                    .setTopic(existingLesson.get().getTopic()).buildPartial();

            if (existingLesson.get().getHomework() != null)
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

        } catch (Exception e) {
            response.onError(
                    new Throwable(e.getMessage())
            );
            response.onCompleted();
        }

    }

    @Override
    public void addAttendance(RequestAddAttendance request, StreamObserver<ResponseAddAttendance> response) {
        String id = request.getLessonId();
        try {
            Optional<LessonEntity> existingLesson = lessonRepository.findById(id);

            if (existingLesson.isEmpty()) {
                throw new IllegalStateException("No existing lesson with id of: " + id);
            }

            List<String> studentUsernames = request.getUsernamesList();
            Set<UserEntity> students = userRepository.findAll().stream()
                .filter(user -> studentUsernames.contains(user.getUsername()))
                .collect(Collectors.toSet());

            if (students.isEmpty()) {
                throw new IllegalStateException("No user matches given usernames");
            }

            existingLesson.get().setAttendance(students);
            lessonRepository.save(existingLesson.get());

            response.onNext(ResponseAddAttendance.newBuilder().setAmountOfParticipants(students.size()).build());
            response.onCompleted();

        } catch (Exception e) {
            response.onError(new Throwable("An error occurred: " + e.getMessage()));
            response.onCompleted();

        }
    }
    @Override
    public void getLessonsByClassId(RequestGetLessonsByClassId request, StreamObserver<ResponseGetLessonsByClassId> response) {
        try {
            String classId = request.getClassId();

            List<LessonEntity> lessons = lessonRepository.findByClassId(classId);
            List<LessonData> grpcLessons = new ArrayList<>();


            for (LessonEntity lessonEntity : lessons) {
                LessonData lesson = LessonData.newBuilder()
                        .setId(lessonEntity.getId())
                        .setDate(lessonEntity.getDate())
                        .setTopic(lessonEntity.getTopic())
                        .setDescription(lessonEntity.getDescription()).buildPartial();

                if (lessonEntity.getHomework() != null) {
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
        } catch (Exception e) {
            response.onError(new Throwable(e.getMessage()));
            response.onCompleted();
        }
    }



    public void deleteLesson(RequestDeleteLesson request, StreamObserver<ResponseDeleteLesson> response) {
        try {
            String lessonId = request.getLessonId();
            Optional<LessonEntity> lesson = lessonRepository.findById(lessonId);
            if (lesson.isEmpty())
                throw new IllegalStateException("No exisitng lesson with id of: " + lessonId);

             lessonRepository.deleteById(lessonId);

              response.onNext(
                    ResponseDeleteLesson.newBuilder()
                            .setStatus(ResponseDeleteLesson.Status.OK)
                            .setMessage("Lesson deleted successfully")
                            .build()
            );
            response.onCompleted();
        } catch (Exception e) {
            response.onError(
                    new StatusRuntimeException(Status.INTERNAL.withDescription("Error deleting lesson: " + e.getMessage()))
            );
            response.onCompleted();
        }
    }


}


