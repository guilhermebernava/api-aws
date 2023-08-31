using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Order : Entity
{
    public Order(Guid userId)
    {
        UserId = userId;
    }

    public virtual IList<OrderItem> Items { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
}
