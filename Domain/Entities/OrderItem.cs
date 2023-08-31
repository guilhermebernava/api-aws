using System.Text.Json.Serialization;

namespace Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
    public Guid OrderId { get; set; } = Guid.Empty;
    public int Quantity { get; set; }
}
