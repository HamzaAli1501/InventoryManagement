namespace InventoryManagement.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public decimal? Price { get; set; }

    public string? ProductDetails { get; set; }

    public virtual ICollection<MainStock> MainStocks { get; set; } = new List<MainStock>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}