using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Sales_NET8.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string TaxId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
