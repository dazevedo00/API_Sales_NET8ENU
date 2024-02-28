using API_Sales_NET8.Models;
using API_Sales_NET8.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System;
using API_Sales_NET8.DTOs;

public class SalesItemService : ISalesItemService
{
    private readonly AppDbContext _context;
    private readonly ILogger<SalesItemService> _logger;
    private readonly IMapper _mapper;

    public SalesItemService(AppDbContext context, ILogger<SalesItemService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IActionResult> GetSalesItemsAsync()
    {
        try
        {
            var salesItems = await _context.SalesItems.ToListAsync();
            return new OkObjectResult(salesItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving sales items.");
            return new StatusCodeResult(500); // Internal Server Error
        }
    }

    public async Task<IActionResult> GetSalesItemAsync(string code, string salesUnit)
    {
        try
        {
            var salesItem = await _context.SalesItems
                .Where(item => item.Code == code && item.SalesUnit == salesUnit)
                .FirstOrDefaultAsync();

            if (salesItem == null)
            {
                return new NotFoundObjectResult("Sales item not found");
            }

            return new OkObjectResult(salesItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while retrieving sales item with code {code} and sales unit {salesUnit}.");
            return new StatusCodeResult(500); // Internal Server Error
        }
    }

    public async Task<IActionResult> CreateSalesItemAsync(SalesItem salesItem)
    {
        try
        {
            // Validate price
            if (!IsPriceValid(salesItem.Price))
            {
                return new BadRequestObjectResult("Price must be greater than 0");
            }

            // Validate if SalesItem with the same ID and SalesUnit already exists
            var existingSalesItem = await GetSalesItemByIdAndUnit(salesItem.Code, salesItem.SalesUnit);
            if (existingSalesItem != null)
            {
                return new ConflictObjectResult("Sales item with the same ID and SalesUnit already exists");
            }

            salesItem.CreatedAt = DateTime.Now;
            salesItem.UpdatedAt = DateTime.Now;

            _context.SalesItems.Add(salesItem);
            await _context.SaveChangesAsync();

            var createdSalesItem = _mapper.Map<SalesItemDto>(salesItem); 
            return new CreatedAtActionResult(nameof(GetSalesItemAsync), "SalesItem", new { code = createdSalesItem.Code, salesUnit = createdSalesItem.SalesUnit }, createdSalesItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating a new sales item.");
            return new StatusCodeResult(500); // Internal Server Error
        }
    }

    public async Task<IActionResult> UpdateSalesItemAsync(string code, string salesUnit, SalesItem updatedSalesItem)
    {
        try
        {
            var existingSalesItem = await GetSalesItemByIdAndUnit(code, salesUnit);
            if (existingSalesItem == null)
            {
                return new NotFoundObjectResult("Sales item not found");
            }

            if (!IsPriceValid(updatedSalesItem.Price))
            {
                return new BadRequestObjectResult("Price must be greater than 0");
            }

            // Update properties with new values
            existingSalesItem.Description = updatedSalesItem.Description;
            existingSalesItem.Price = updatedSalesItem.Price;
            existingSalesItem.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new OkObjectResult(existingSalesItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while updating sales item with code {code} and sales unit {salesUnit}.");
            return new StatusCodeResult(500); // Internal Server Error
        }
    }

    public async Task<IActionResult> DeleteSalesItemAsync(string code, string salesUnit)
    {
        try
        {
            var salesItemToDelete = await GetSalesItemByIdAndUnit(code, salesUnit);
            if (salesItemToDelete == null)
            {
                return new NotFoundObjectResult("Sales item not found");
            }

            _context.SalesItems.Remove(salesItemToDelete);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while deleting sales item with code {code} and sales unit {salesUnit}.");
            return new StatusCodeResult(500); // Internal Server Error
        }
    }

    #region Private Tasks
    private async Task<SalesItem> GetSalesItemByIdAndUnit(string code, string salesUnit)
    {
        return await _context.SalesItems
            .Where(item => item.Code == code && item.SalesUnit == salesUnit)
            .FirstOrDefaultAsync();
    }

    private static bool IsPriceValid(decimal price) => price > 0;
    #endregion
}
