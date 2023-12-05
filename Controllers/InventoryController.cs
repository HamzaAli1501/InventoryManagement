using InventoryManagement.Interface;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

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
        ApiResponse resp = new ApiResponse();
        if (!ModelState.IsValid)
        {
            resp.Result = false;
            resp.Message = "Invalid Model";
            return resp;
        }

        resp = _service.CreateNewPurchase(obj);
        return resp.Result ? resp : new ApiResponse();
    }

    #endregion

    #region Sales

    [HttpGet("getAllSale")]
    public ApiResponse GetAllSale()
    {
        var resp = _service.GetAllSale();
        if (resp.Result)
        {
            return resp;
        }
        else
        {
            return new ApiResponse();
        }
    }

    [HttpPost("createNewSale")]
    public ApiResponse CreateNewSale([FromBody] Sale obj)
    {
        ApiResponse resp = new ApiResponse();
        if (!ModelState.IsValid)
        {
            resp.Result = false;
            resp.Message = "Invalid Model";
            return resp;
        }

        resp = _service.CreateNewSale(obj);
        if (resp.Result)
        {
            return resp;
        }
        else
        {
            return new ApiResponse();
        }
    }

    #endregion

    #region Stock

    #endregion

    // [HttpGet("getDashboardData")]
    // public ApiResponse getDashboardData()
    // {
    //     var resp = _service.GetDashboardData();
    //     if (resp.Result)
    //     {
    //         return resp;
    //     }
    //     else
    //     {
    //         return new ApiResponse();
    //     }
    // }
}