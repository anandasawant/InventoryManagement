using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
