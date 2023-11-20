package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.HandInHomework;

public interface IHandInHomeworkRepository extends JpaRepository<HandInHomework, String>
{
}
