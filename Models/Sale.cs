namespace InventoryManagement.Models;

public sealed partial class Sale
{
    public int SaleId { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? MobileNo { get; set; }

    public DateTime? SaleDate { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalAmount { get; set; }

    public Product? Product { get; set; }
}