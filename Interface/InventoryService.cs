using InventoryManagement.Models;

namespace InventoryManagement.Interface;

public class InventoryService : IInventoryService
{
    private readonly InventoryManagementContext _context;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(ILogger<InventoryService> logger, InventoryManagementContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void CheckStockByProductId(int id)
    {
        throw new NotImplementedException();
    }

    public void CreateNewProduct()
    {
        throw new NotImplementedException();
    }

    public ApiResponse CreateNewPurchase(Purchase obj)
    {
        ApiResponse res = new();
        var isDuplicate = _context.Purchases.SingleOrDefault(x =>
            obj.InvoiceNumber != null && x.InvoiceNumber != null &&
            string.Equals(x.InvoiceNumber,
                obj.InvoiceNumber,
                StringComparison.OrdinalIgnoreCase));
        if (isDuplicate is null)
        {
            _context.Purchases.Add(obj);
            _context.SaveChanges();

            var isStockExists = _context.MainStocks.SingleOrDefault(x => x.ProductId == obj.ProductId);
            if (isStockExists is null)
            {
                MainStock stock = new()
                {
                    Quantity = obj.Quantity,
                    ProductId = obj.ProductId,
                    Product = obj.Product,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _context.MainStocks.Add(stock);
                _context.SaveChanges();
            }
            else
            {
                isStockExists.Quantity += obj.Quantity;
                isStockExists.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
            }

            res.Result = true;
            res.Message = "Purchased successfully";
            return res;
        }
        else
        {
            res.Result = false;
            res.Message = "Invoice already exist";
        }

        ApiResponse resp = new ApiResponse();

        return resp;
    }

    public ApiResponse CreateNewSale(Sale obj)
    {
        return new ApiResponse();
    }

    public ApiResponse GetAllProducts()
    {
        try
        {
            var resp = new ApiResponse()
            {
                Message = "Products data",
                Result = true,
                Data = _context.Products.AsEnumerable()
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            throw;
        }
    }

    public ApiResponse GetAllPurchase()
    {
        try
        {
            var purchased = (
                from purchase in _context.Purchases
                join product in _context.Products on purchase.ProductId equals product.ProductId
                select new
                {
                    invoiceAmount = purchase.InvoiceAmount,
                    invoiceNumber = purchase.InvoiceNumber,
                    purchase.ProductId,
                    purchasedDate = purchase.PurchaseDate,
                    purchaseId = purchase.PurchaseId,
                    quantity = purchase.Quantity,
                    supplierName = purchase.SupplierName,
                    productName = product.ProductName,
                });

            var resp = new ApiResponse()
            {
                Message = "Purchase data",
                Result = true,
                Data = purchased
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            throw;
        }
    }

    public ApiResponse GetAllSale()
    {
        try
        {
            var resp = new ApiResponse()
            {
                Message = "Sales data",
                Result = true,
                Data = _context.Sales.AsEnumerable()
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            throw;
        }
    }

    public ApiResponse GetAllStock()
    {
        try
        {
            var resp = new ApiResponse()
            {
                Message = "Products data",
                Result = true,
                Data = _context.MainStocks.AsEnumerable()
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            throw;
        }
    }

    public ApiResponse GetDashboardData()
    {
        try
        {
            var resp = new ApiResponse()
            {
                Message = "Dashboard data",
                Result = true,
                Data = _context.MainStocks.AsEnumerable()
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            throw;
        }
    }
}