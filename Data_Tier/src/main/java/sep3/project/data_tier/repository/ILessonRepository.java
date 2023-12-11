package sep3.project.data_tier.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import sep3.project.data_tier.entity.LessonEntity;

import java.util.List;
@Repository
public interface ILessonRepository extends JpaRepository<LessonEntity, String>
{
}
