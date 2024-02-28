using API_Sales_NET8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Sales_NET8.Services
{
    public interface ISalesItemService
    {
        Task<IActionResult> GetSalesItemsAsync();
        Task<IActionResult> GetSalesItemAsync(string code, string salesUnit);
        Task<IActionResult> CreateSalesItemAsync(SalesItem salesItem);
        Task<IActionResult> UpdateSalesItemAsync(string code, string salesUnit, SalesItem updatedSalesItem);
        Task<IActionResult> DeleteSalesItemAsync(string code, string salesUnit);
    }
}
