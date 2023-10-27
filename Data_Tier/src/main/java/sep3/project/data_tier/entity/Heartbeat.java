package sep3.project.data_tier.entity;

import jakarta.persistence.*;

import java.io.Serial;

@Entity
@Table
public class Heartbeat
{
  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  @Column
  private int id;
  @Column
  private String pulse;
  public Heartbeat(String pulse)
  {
    this.pulse=pulse;
  }
  public Heartbeat()
  {

  }

  public int getId()
  {
    return id;
  }

  public String getPulse()
  {
    return pulse;
  }

  public void setPulse(String pulse)
  {
    this.pulse = pulse;
  }
}
