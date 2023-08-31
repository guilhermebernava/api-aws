using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Product : Entity
{
    public Product(double value, string name, Guid userId) : base()
    {
        Value = value;
        Name = name;
        UserId = userId;
    }

    public double Value { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; }
    [JsonIgnore]
    public virtual OrderItem OrderItem { get; set; }
}
