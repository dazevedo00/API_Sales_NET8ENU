using API_Sales_NET8.Models;
using API_Sales_NET8.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Sales_NET8.Controllers
{
    [ApiController]
    [Route("api/salesitems")]
    public class SalesItemController(ISalesItemService salesItemService) : ControllerBase
    {
        private readonly ISalesItemService _salesItemService = salesItemService;

        [HttpGet]
        public async Task<IActionResult> GetSalesItems()
        {
            return await _salesItemService.GetSalesItemsAsync();
        }

        [HttpGet("{code}/{salesUnit}")]
        public async Task<IActionResult> GetSalesItem(string code, string salesUnit)
        {
            return await _salesItemService.GetSalesItemAsync(code, salesUnit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesItem(SalesItem salesItem)
        {
            return await _salesItemService.CreateSalesItemAsync(salesItem);
        }

        [HttpPut("{code}/{salesUnit}")]
        public async Task<IActionResult> UpdateSalesItem(string code, string salesUnit, SalesItem updatedSalesItem)
        {
            return await _salesItemService.UpdateSalesItemAsync(code, salesUnit, updatedSalesItem);
        }

        [HttpDelete("{code}/{salesUnit}")]
        public async Task<IActionResult> DeleteSalesItem(string code, string salesUnit)
        {
            return await _salesItemService.DeleteSalesItemAsync(code, salesUnit);
        }
    }
}