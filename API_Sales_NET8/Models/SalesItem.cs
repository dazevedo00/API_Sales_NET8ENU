using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Sales_NET8.Models
{
    public class SalesItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Este atributo é necessário se estiver usando Identity como a estratégia para gerar valores para a chave primária
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string SalesUnit { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
