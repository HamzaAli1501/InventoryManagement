using InventoryManagement.Models;

namespace InventoryManagement.Service;

public interface IInventoryService
{
    public ApiResponse GetAllProducts();
    public ApiResponse CreateNewProduct(Product obj);

    public ApiResponse GetAllPurchase();
    public ApiResponse CreateNewPurchase(Purchase obj);

    public ApiResponse GetAllSale();
    public ApiResponse CreateNewSale(Sale obj);
    public ApiResponse GetAllStocks();
    public ApiResponse CheckStockByProductId(int id);
}