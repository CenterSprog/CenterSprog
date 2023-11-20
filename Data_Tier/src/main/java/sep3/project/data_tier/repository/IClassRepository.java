package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.ClassEntity;

public interface IClassRepository extends JpaRepository<ClassEntity, String>
{
}
