package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import com.google.protobuf.Empty;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;

@GrpcService
public class HeartbeatServiceImpl extends HeartbeatServiceGrpc.HeartbeatServiceImplBase
{
  private final static Logger LOG = LoggerFactory.getLogger(HeartbeatServiceImpl.class);
  private static List<String> HEARTBEAT_DATA_LIST = new ArrayList<String>();

  @Override
  public void getHeartbeat(Empty request, StreamObserver<ResponseGetHeartbeats> responseObserver)
  {
    LOG.info("Get Heartbeat");
    int listSize = HEARTBEAT_DATA_LIST.size();
    ResponseGetHeartbeats heartbeats = ResponseGetHeartbeats.newBuilder().setPulses(listSize).build();
    LOG.info(heartbeats.toString());
    responseObserver.onNext(heartbeats);
    responseObserver.onCompleted();
  }

  @Override
  public void createHeartbeat(RequestCreateHeartbeat request, StreamObserver<ResponseCreateHeartbeat> responseObserver){
    LOG.info("Create Heartbeat");
    ResponseCreateHeartbeat responseCreateHeartbeat = ResponseCreateHeartbeat.newBuilder().setPulse(request.getPulse()).build();
    HEARTBEAT_DATA_LIST.add(responseCreateHeartbeat.getPulse());
    responseObserver.onNext(responseCreateHeartbeat);
    responseObserver.onCompleted();
  }
}
