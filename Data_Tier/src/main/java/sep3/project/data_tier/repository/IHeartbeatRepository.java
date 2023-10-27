package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.Heartbeat;

public interface IHeartbeatRepository extends JpaRepository<Heartbeat, Integer>
{
}
