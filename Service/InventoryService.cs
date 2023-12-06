using InventoryManagement.Models;

namespace InventoryManagement.Service;

public class InventoryService : IInventoryService
{
    private readonly InventoryManagementContext _context;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(ILogger<InventoryService> logger, InventoryManagementContext context)
    {
        _logger = logger;
        _context = context;
    }


    #region Stock API

    public ApiResponse GetAllStocks()
    {
        try
        {
            var stocks =
            (
                from stock in _context.MainStocks
                join product in _context.Products on stock.ProductId equals product.ProductId
                select new
                {
                    stock.CreatedDate,
                    stock.ModifiedDate,
                    stock.ProductId,
                    stock.Quantity,
                    product.ProductName,
                    stock.StockId
                });
            var res = new ApiResponse()
            {
                Result = true,
                Message = "Stock data",
                Data = stocks
            };
            return res;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            return new ApiResponse() { Result = false, Message = e.Message };
            //throw;
        }
    }

    #endregion

    public ApiResponse CheckStockByProductId(int id)
    {
        try
        {
            var resp = new ApiResponse();
            var isStockExist = _context.MainStocks.SingleOrDefault(x => x.ProductId == id);
            if (isStockExist is null) return new ApiResponse() { Message = "Stock Not Available", Result = false };
            if (isStockExist.Quantity != 0)
            {
                resp.Result = true;
                resp.Data = isStockExist;
                resp.Message = "Stock Available";
            }
            else
            {
                resp.Result = false;
                resp.Message = "Stock Not Available";
            }

            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            return new ApiResponse() { Result = false, Message = e.Message };
            //throw;
        }
    }

    #region Product API

    public ApiResponse GetAllProducts()
    {
        try
        {
            var products =
                _context.Products.AsEnumerable();
            var resp = new ApiResponse()
            {
                Message = "Products data",
                Result = true,
                Data = products
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            return new ApiResponse() { Result = false, Message = e.Message };
        }
    }

    public ApiResponse CreateNewProduct(Product obj)
    {
        ApiResponse res = new();
        var isDuplicate = _context.Products.SingleOrDefault(x =>
            obj.ProductName != null && x.ProductName != null &&
            string.Equals(x.ProductName,
                obj.ProductName,
                StringComparison.OrdinalIgnoreCase));
        var isStockExists = _context.MainStocks.SingleOrDefault(x => x.ProductId == obj.ProductId);

        if (isDuplicate is null)
        {
            _context.Products.Add(obj);
            _context.SaveChanges();

            res.Data = isStockExists;
            res.Result = true;
            res.Message = "Product added successfully";
            return res;
        }
        else
        {
            res.Result = false;
            res.Message = "Product already exist";
        }

        return res;
    }

    #endregion

    #region Purchase API

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
            return new ApiResponse() { Result = false, Message = e.Message };
            //throw;
        }
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
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Quantity = obj.Quantity,
                    ProductId = obj.ProductId
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
            res.Data = isStockExists;
            return res;
        }
        else
        {
            res.Result = false;
            res.Message = "Invoice already exist";
        }

        return res;
    }

    #endregion

    #region Sale API

    public ApiResponse GetAllSale()
    {
        try
        {
            var sales = (
                from sale in _context.Sales
                join product in _context.Products on sale.ProductId equals product.ProductId
                select new
                {
                    sale.MobileNo,
                    sale.InvoiceNumber,
                    sale.ProductId,
                    sale.Quantity,
                    sale.SaleDate,
                    sale.TotalAmount,
                    sale.SaleId,
                    product.ProductName,
                    sale.CustomerName,
                });

            var resp = new ApiResponse()
            {
                Message = "Sale data",
                Result = true,
                Data = sales
            };
            return resp;
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occured:{EMessage}", e.Message);
            return new ApiResponse() { Result = false, Message = e.Message };
            //throw;
        }
    }

    public ApiResponse CreateNewSale(Sale obj)
    {
        ApiResponse res = new();
        var isDuplicate = _context.Sales.SingleOrDefault(x =>
            obj.InvoiceNumber != null && x.InvoiceNumber != null &&
            string.Equals(x.InvoiceNumber,
                obj.InvoiceNumber,
                StringComparison.OrdinalIgnoreCase));
        var isStockExists = _context.MainStocks.SingleOrDefault(x => x.ProductId == obj.ProductId);

        if (isDuplicate is null && isStockExists is not null)
        {
            _context.Sales.Add(obj);
            _context.SaveChanges();

            isStockExists.Quantity -= obj.Quantity;
            isStockExists.ModifiedDate = DateTime.Now;
            _context.SaveChanges();

            res.Data = isStockExists;
            res.Result = true;
            res.Message = "Object sold successfully";
            return res;
        }
        else
        {
            res.Result = false;
            res.Message = "Invoice already exist";
        }

        return res;
    }

    #endregion
}