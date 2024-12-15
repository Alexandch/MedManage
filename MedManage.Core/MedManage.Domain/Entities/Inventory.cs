namespace MedManage.Domain.Entities;

public class Inventory
{
    public int QuantityInStock { get; set; }
    public DateTime LastUpdated { get; set; }
    public Guid ProductId { get; set; } // Это внешний ключ для связи с Product
    public Product Product { get; set; } // Навигационное свойство для связи с Product
}
