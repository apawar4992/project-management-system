using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public int? Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}
