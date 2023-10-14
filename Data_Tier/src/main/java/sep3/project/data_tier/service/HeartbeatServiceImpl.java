package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import com.google.protobuf.Empty;
import net.devh.boot.grpc.server.service.GrpcService;
import sep3.project.protobuf.*;

import java.util.List;

@GrpcService
public class HeartbeatServiceImpl extends HeartbeatServiceGrpc.HeartbeatServiceImplBase
{
  private final static List<HeartbeatData> HEARTBEAT_DATA_LIST = List.of();

  @Override
  public void getHeartbeat(Empty request, StreamObserver<ResponseGetHeartbeats> responseObserver)
  {
    ResponseGetHeartbeats heartbeats = ResponseGetHeartbeats.newBuilder().addAllPulses(HEARTBEAT_DATA_LIST).build();
    responseObserver.onNext(heartbeats);
    responseObserver.onCompleted();
  }

  @Override
  public void createHeartbeat(RequestCreateHeartbeat request, StreamObserver<ResponseCreateHeartbeat> responseObserver){
    HEARTBEAT_DATA_LIST.add(request.getData());
    ResponseCreateHeartbeat responseCreateHeartbeat = ResponseCreateHeartbeat.newBuilder().setData(request.getData()).build();
    responseObserver.onNext(responseCreateHeartbeat);
    responseObserver.onCompleted();
  }
}
