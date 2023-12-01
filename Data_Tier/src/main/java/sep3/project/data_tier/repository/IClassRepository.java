package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import sep3.project.data_tier.entity.ClassEntity;

import java.util.List;
import java.util.Optional;

public interface IClassRepository extends JpaRepository<ClassEntity, String>
{
  List<ClassEntity> findByUsers_Username(String username);


}
