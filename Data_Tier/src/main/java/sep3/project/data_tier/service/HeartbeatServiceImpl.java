package sep3.project.data_tier.service;

import io.grpc.stub.StreamObserver;
import com.google.protobuf.Empty;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import sep3.project.data_tier.entity.Heartbeat;
import sep3.project.data_tier.repository.IHeartbeatRepository;
import sep3.project.protobuf.*;

import java.util.ArrayList;
import java.util.List;

@GrpcService
public class HeartbeatServiceImpl extends HeartbeatServiceGrpc.HeartbeatServiceImplBase
{
  private IHeartbeatRepository heartbeatRepository;

  public HeartbeatServiceImpl(IHeartbeatRepository heartbeatRepository){
    this.heartbeatRepository=heartbeatRepository;
  }
  private final static Logger LOG = LoggerFactory.getLogger(HeartbeatServiceImpl.class);

  @Override
  public void getHeartbeat(Empty request, StreamObserver<ResponseGetHeartbeats> responseObserver)
  {
    LOG.info("Get Heartbeat");
    ResponseGetHeartbeats heartbeats = ResponseGetHeartbeats.newBuilder().setPulses(heartbeatRepository.findAll().size()).build();
    LOG.info(heartbeats.toString());
    responseObserver.onNext(heartbeats);
    responseObserver.onCompleted();
  }

  @Override
  public void createHeartbeat(RequestCreateHeartbeat request, StreamObserver<ResponseCreateHeartbeat> responseObserver){
    LOG.info("Create Heartbeat");
    Heartbeat heartbeat = new Heartbeat(request.getPulse());
    heartbeatRepository.save(heartbeat);
    ResponseCreateHeartbeat responseCreateHeartbeat = ResponseCreateHeartbeat.newBuilder().setPulse(heartbeat.getPulse()).build();
    responseObserver.onNext(responseCreateHeartbeat);
    responseObserver.onCompleted();
  }
}
