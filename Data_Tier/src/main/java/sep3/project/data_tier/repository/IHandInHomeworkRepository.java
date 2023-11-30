package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.HandInHomeworkEntity;
import sep3.project.data_tier.entity.HandInHomeworkEntity;

public interface IHandInHomeworkRepository extends JpaRepository<HandInHomeworkEntity, String>
{
}
