package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.FeedbackEntity;

import java.util.Optional;

public interface IFeedbackRepository extends JpaRepository<FeedbackEntity, String>
{
}
