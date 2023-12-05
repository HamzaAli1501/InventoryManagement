namespace InventoryManagement.Models;

public class ApiResponse
{
    public string Message { get; set; } = string.Empty;
    public bool Result { get; set; } = false;
    public dynamic? Data { get; set; }
}