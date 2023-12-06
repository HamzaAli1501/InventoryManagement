using InventoryManagement.Models;
using InventoryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    #region Product API

    [HttpGet("getAllProducts")]
    public ApiResponse GetAllProducts()
    {
        var resp = _service.GetAllProducts();
        return resp.Result ? resp : new ApiResponse();
    }

    [HttpPost("createNewProducts")]
    public ApiResponse CreateNewProducts([FromBody] Product obj)
    {
        if (!ModelState.IsValid)
        {
            return new ApiResponse()
            {
                Result = false,
                Message = "Invalid Product Object"
            };
        }

        var resp = _service.CreateNewProduct(obj);
        return resp.Result ? resp : new ApiResponse();
    }

    #endregion


    #region Purchase API

    [HttpGet("getAllPurchase")]
    public ApiResponse GetAllPurchase()
    {
        var resp = _service.GetAllPurchase();
        return resp.Result ? resp : new ApiResponse();
    }

    [HttpPost("createNewPurchase")]
    public ApiResponse CreateNewPurchase([FromBody] Purchase obj)
    {
        if (!ModelState.IsValid)
        {
            return new ApiResponse()
            {
                Result = false,
                Message = "Invalid Purchase Model"
            };
        }

        var resp = _service.CreateNewPurchase(obj);
        return resp.Result ? resp : new ApiResponse();
    }

    #endregion

    #region Sales

    [HttpGet("getAllSale")]
    public ApiResponse GetAllSale()
    {
        var resp = _service.GetAllSale();
        return resp.Result ? resp : new ApiResponse();
    }

    [HttpPost("createNewSale")]
    public ApiResponse CreateNewSale([FromBody] Sale obj)
    {
        if (!ModelState.IsValid)
        {
            return new ApiResponse()
            {
                Result = false,
                Message = "Invalid Purchase Model"
            };
        }

        var resp = _service.CreateNewSale(obj);
        return resp.Result ? resp : new ApiResponse();
    }

    #endregion

    #region Stock

    [HttpGet("getAllStock")]
    public ApiResponse GetAllStock()
    {
        var resp = _service.GetAllStocks();
        return resp.Result ? resp : new ApiResponse();
    }

    [HttpPost("checkStockByProductId")]
    public ApiResponse CheckStockByProductId(int productId)
    {
        var resp = _service.CheckStockByProductId(productId);
        return resp.Result ? resp : new ApiResponse();
    }

    #endregion
}