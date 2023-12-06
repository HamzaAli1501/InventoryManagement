namespace InventoryManagement.Models;

public sealed partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public decimal? Price { get; set; }

    public string? ProductDetails { get; set; }

    public ICollection<MainStock> MainStocks { get; set; } = new List<MainStock>();

    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}