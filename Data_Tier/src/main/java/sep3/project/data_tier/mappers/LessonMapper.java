package sep3.project.data_tier.mappers;


import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;
import sep3.project.data_tier.entity.HomeworkEntity;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.protobuf.Homework;
import sep3.project.protobuf.LessonData;

@Mapper
public interface LessonMapper {
    LessonMapper INSTANCE = Mappers.getMapper(LessonMapper.class);
    LessonData toProto(LessonEntity lessonEntity);
    LessonEntity toEntity(LessonData lesson);

}
