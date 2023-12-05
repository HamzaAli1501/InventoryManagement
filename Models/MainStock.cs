namespace InventoryManagement.Models;

public partial class MainStock
{
    public int StockId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product? Product { get; set; }
}