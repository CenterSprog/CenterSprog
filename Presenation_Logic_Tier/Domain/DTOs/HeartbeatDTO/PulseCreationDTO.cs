namespace Domain.DTOs.HeartbeatDTO;

public class PulseCreationDTO
{
    public string Pulse { get; }

    public PulseCreationDTO(string pulse)
    {
        Pulse = pulse;
    }
}