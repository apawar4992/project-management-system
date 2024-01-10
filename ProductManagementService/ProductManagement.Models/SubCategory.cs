using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
