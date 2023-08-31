using System.Text.Json.Serialization;

namespace Domain.Entities;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    [JsonIgnore]
    public DateTime CreatedAt { get;  set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get;  set; }
    [JsonIgnore]
    public DateTime? DeletedAt { get;  set; }
}
