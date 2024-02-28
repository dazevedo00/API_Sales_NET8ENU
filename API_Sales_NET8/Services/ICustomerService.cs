using API_Sales_NET8.DTOs;
using API_Sales_NET8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Sales_NET8.Services
{
    public interface ICustomerService
    {
        Task<IActionResult> GetCustomersAsync();
        Task<IActionResult> GetCustomerAsync(string code);
        Task<IActionResult> CreateCustomerAsync(CustomerDto customer);
        Task<IActionResult> UpdateCustomerAsync(string code, CustomerDto updatedCustomer);
        Task<IActionResult> DeleteCustomerAsync(string code);
    }

}
