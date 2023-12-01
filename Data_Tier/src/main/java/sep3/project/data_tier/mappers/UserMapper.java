package sep3.project.data_tier.mappers;

import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;
import sep3.project.data_tier.entity.UserEntity;
import sep3.project.protobuf.UserData;

@Mapper
public interface UserMapper {
    UserMapper INSTANCE = Mappers.getMapper(UserMapper.class);
    UserData toProto(UserEntity userEntity);

    UserEntity toEntity(UserData userData);
}
