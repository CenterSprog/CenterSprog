package sep3.project.data_tier.mappers;

import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;
import sep3.project.protobuf.ClassEntity;

@Mapper
public interface ClassMapper
{
  ClassMapper INSTANCE = Mappers.getMapper(ClassMapper.class);
  ClassEntity toProto(sep3.project.data_tier.entity.ClassEntity classEntity);

  sep3.project.data_tier.entity.ClassEntity toEntity(ClassEntity classEntity);

}
