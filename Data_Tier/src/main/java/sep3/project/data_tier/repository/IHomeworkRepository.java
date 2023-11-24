package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.HomeworkEntity;

public interface IHomeworkRepository extends JpaRepository<HomeworkEntity, String>
{
}
