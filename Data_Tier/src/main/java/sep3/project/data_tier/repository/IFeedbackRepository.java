package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.Feedback;

public interface IFeedbackRepository extends JpaRepository<Feedback, String>
{
}
