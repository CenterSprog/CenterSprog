using Application.DAOInterfaces;
using Domain.Models;
using Grpc.Net.Client;
using gRPCClient;

namespace Application.gRPCClients;

public class HeartbeatClient:IHeartbeatDAO
{
    public async Task<String> CreateAsync(Heartbeat heartbeat)
    {
        
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new HeartbeatService.HeartbeatServiceClient(channel);
        
        var request = new RequestCreateHeartbeat()
        {
            Pulse = heartbeat.Pulse,
        };


        var reply = await client.createHeartbeatAsync(request);

        return await Task.FromResult(reply.Pulse);
    }

    public async Task<int> GetAsync()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new HeartbeatService.HeartbeatServiceClient(channel);
        var reply = await client.getHeartbeatAsync(null);

        return await Task.FromResult(reply.Pulses);
    }
}