package sep3.project.data_tier.mappers;

import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;
import sep3.project.data_tier.entity.LessonEntity;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.protobuf.Lesson;
import sep3.project.protobuf.UserParticipant;

@Mapper
public interface ParticipantMapper {
    ParticipantMapper INSTANCE = Mappers.getMapper(ParticipantMapper.class);
    UserParticipant toProto(UserEntity userEntity);
    UserEntity toEntity(UserParticipant userParticipant);
}
