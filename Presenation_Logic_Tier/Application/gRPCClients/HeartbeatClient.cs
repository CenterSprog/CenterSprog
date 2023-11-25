using Application.DAOInterfaces;
using Application.Logic;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using gRPCClient;

namespace Application.gRPCClients;

public class HeartbeatClient:IHeartbeatDAO
{
    public async Task<String> CreateAsync(Heartbeat heartbeat)
    {
        
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new HeartbeatService.HeartbeatServiceClient(channel);

        var request = new RequestCreateHeartbeat
        {
            Pulse = heartbeat.Pulse,
        };
        var reply = new ResponseCreateHeartbeat();
        Console.WriteLine("Going to call Java");
        try
        {
             reply = await client.CreateHeartbeatAsync(request);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }   
        return await Task.FromResult(reply.Pulse);
    }

    public async Task<int> GetAsync()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new HeartbeatService.HeartbeatServiceClient(channel);
        var reply = await client.GetHeartbeatAsync(new Empty());

        return await Task.FromResult(reply.Pulses);
    }
}