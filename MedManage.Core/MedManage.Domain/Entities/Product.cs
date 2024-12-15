using MedManage.Domain.Enums;

namespace MedManage.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Inventory Inventory { get; set; }
}
