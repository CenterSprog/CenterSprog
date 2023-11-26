package sep3.project.data_tier.configs;

import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import sep3.project.data_tier.entity.ClassEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.entity.UserEntity;
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
            UserEntity s2 = new UserEntity("julija","julija","Julija","Gramovica","julijagr@gmail.com","student");
            UserEntity t2 = new UserEntity("joseph","steffan","Joseph","Okika","joseph@via.dk","teacher");
            UserEntity t3 = new UserEntity("jakob","jakob","Jakob","Lalassen","jakob@via.dk","teacher");


            LessonEntity l1 = new LessonEntity(12312312,"grpc","awesome lesson");
            LessonEntity l2 = new LessonEntity(12312313,"rabit-mq","what da hell is going on");

            ClassEntity c1 = new ClassEntity("sdj1","c05.16b");
            ClassEntity c2 = new ClassEntity("sep3","c05.14b");


            c1.addUser(s1);
            c1.addUser(t1);
            c1.addUser(t2);

            c2.addUser(s2);
            c2.addUser(t2);
            c2.addUser(t3);

            c1.addLesson(l1);
            c1.addLesson(l2);


            userRepository.save(s1);
            userRepository.save(t1);

            userRepository.save(s2);
            userRepository.save(t2);

            userRepository.save(t3);

            lessonRepository.save(l1);
            lessonRepository.save(l2);

            classRepository.save(c1);
            classRepository.save(c2);



        };
    }
}
