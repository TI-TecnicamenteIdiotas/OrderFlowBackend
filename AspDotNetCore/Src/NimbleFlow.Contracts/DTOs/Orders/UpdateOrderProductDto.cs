namespace NimbleFlow.Contracts.DTOs.Orders;

public class UpdateOrderProductDto
{
    public Guid? OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public int? ProductAmount { get; set; }
}