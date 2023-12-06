namespace InventoryManagement.Models;

public sealed partial class MainStock
{
    public int StockId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Product? Product { get; set; }
}