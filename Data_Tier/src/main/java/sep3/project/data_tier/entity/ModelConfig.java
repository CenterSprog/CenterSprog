package sep3.project.data_tier.entity;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import sep3.project.data_tier.repository.*;

@Configuration
public class ModelConfig {
    @Bean
    CommandLineRunner productCommandLineRunner(
            IClassRepository classRepository,
            IFeedbackRepository feedbackRepository,
            IHandInHomeworkRepository handInHomeworkRepository,
            IHomeworkRepository homeworkRepository,
            IUserRepository userRepository,
            ILessonRepository lessonRepository
    ){
        return args -> {
//            set up model here
            UserEntity s1 = new UserEntity("damian","damian","Damian","Trafialek","damian.trafialek@gmail.com","student");
            UserEntity t1 = new UserEntity("steffan","steffan","Steffan","Visenberg","sva@via.dk","teacher");

            Lesson l1 = new Lesson(12312312,"grpc","awesome lesson");
            Lesson l2 = new Lesson(12312313,"rabit-mq","what da hell is going on");

            ClassEntity c1 = new ClassEntity("sdj3","c05.16b");

            c1.addUser(s1);
            c1.addUser(t1);

            c1.addLesson(l1);
            c1.addLesson(l2);

            userRepository.save(s1);
            userRepository.save(t1);

            lessonRepository.save(l1);
            lessonRepository.save(l2);

            classRepository.save(c1);



        };
    }
}
