using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be in range {2}-{1} symbols")]
        public string Name { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Length must be in range {2}-{1} symbols")]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 100)]
        public int Qty { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }

    }
}
