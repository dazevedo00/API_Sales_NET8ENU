using API_Sales_NET8.DTOs;
using API_Sales_NET8.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Sales_NET8.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult> GetCustomer(string code)
        {
            var customer = await _customerService.GetCustomerAsync(code);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _customerService.CreateCustomerAsync(customerRequestDto);
        }


        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateCustomer(string code, [FromBody] CustomerDto customerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCustomer = await _customerService.UpdateCustomerAsync(code, customerRequestDto);

            if (updatedCustomer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(updatedCustomer);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteCustomer(string code)
        {
            var deletedCustomer = await _customerService.DeleteCustomerAsync(code);

            if (deletedCustomer == null)
            {
                return NotFound("Customer not found");
            }

            return NoContent();
        }
    }
}
