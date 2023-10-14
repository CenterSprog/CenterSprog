package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import com.google.protobuf.Empty;
import net.devh.boot.grpc.server.service.GrpcService;
import sep3.project.protobuf.*;

import java.util.List;

@GrpcService
public class HeartbeatServiceImpl extends HeartbeatServiceGrpc.HeartbeatServiceImplBase
{
  private final static List<String> HEARTBEAT_DATA_LIST = List.of();

  @Override
  public void getHeartbeat(Empty request, StreamObserver<ResponseGetHeartbeats> responseObserver)
  {
    int listSize = HEARTBEAT_DATA_LIST.size();
    ResponseGetHeartbeats heartbeats = ResponseGetHeartbeats.newBuilder().setPulses(listSize).build();
    responseObserver.onNext(heartbeats);
    responseObserver.onCompleted();
  }

  @Override
  public void createHeartbeat(RequestCreateHeartbeat request, StreamObserver<ResponseCreateHeartbeat> responseObserver){
    HEARTBEAT_DATA_LIST.add(request.getPulse());
    ResponseCreateHeartbeat responseCreateHeartbeat = ResponseCreateHeartbeat.newBuilder().setPulse(request.getPulse()).build();
    responseObserver.onNext(responseCreateHeartbeat);
    responseObserver.onCompleted();
  }
}
