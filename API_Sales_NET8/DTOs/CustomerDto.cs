using System.ComponentModel.DataAnnotations;

namespace API_Sales_NET8.DTOs
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "TaxId is required")]
        public string TaxId { get; set; }
    }
}
