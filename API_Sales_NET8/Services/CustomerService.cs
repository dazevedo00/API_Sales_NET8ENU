using API_Sales_NET8.DTOs;
using API_Sales_NET8.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Strategy_Pattern;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Sales_NET8.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext context, ILogger<CustomerService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
                return new OkObjectResult(customerDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting customers");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        public async Task<IActionResult> GetCustomerAsync(string code)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(p => p.Code == code);

                if (customer == null)
                {
                    return new NotFoundResult();
                }

                var customerDto = _mapper.Map<CustomerDto>(customer);
                return new OkObjectResult(customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting customer with code: {code}");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        public async Task<IActionResult> CreateCustomerAsync(CustomerDto customerRequestDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerRequestDto);

                if (!IsValidPortugueseTaxId(customerRequestDto.Country, customerRequestDto.TaxId))
                {
                    return new BadRequestObjectResult("Invalid Tax ID (NIF)");
                }

                customer.CreatedAt = DateTime.Now;
                customer.UpdatedAt = DateTime.Now;

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(customer);
                return new CreatedAtActionResult(nameof(GetCustomerAsync), "Customer", new { code = customerDto.Code }, customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating customer");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        public async Task<IActionResult> UpdateCustomerAsync(string code, CustomerDto customerRequestDto)
        {
            try
            {
                var existingCustomer = await _context.Customers.FirstOrDefaultAsync(p => p.Code == code);

                if (!IsValidPortugueseTaxId(customerRequestDto.Country, customerRequestDto.TaxId))
                {
                    return new BadRequestObjectResult("Invalid Tax ID (NIF)");
                }

                if (existingCustomer == null)
                {
                    return new NotFoundResult();
                }

                // Use AutoMapper to update existingCustomer properties with customerRequestDto values
                _mapper.Map(customerRequestDto, existingCustomer);

                existingCustomer.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                var updatedCustomerDto = _mapper.Map<CustomerDto>(existingCustomer);
                return new OkObjectResult(updatedCustomerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating customer with code: {code}");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        public async Task<IActionResult> DeleteCustomerAsync(string code)
        {
            try
            {
                var customerToDelete = await _context.Customers.FirstOrDefaultAsync(p => p.Code == code);

                if (customerToDelete == null)
                {
                    return new NotFoundResult();
                }

                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting customer with code: {code}");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        private static bool IsValidPortugueseTaxId(string country, string taxId)
        {
            TaxContext ctx = new(new TaxPT());

            switch (country)
            {
                case "PT":
                    ctx.SetStrategy(new TaxPT());
                    break;
                case "ES":
                    ctx.SetStrategy(new TaxSP());
                    break;
                default:
                    return true;
            }

            return ctx.IsValid(taxId);
        }
    }
}
