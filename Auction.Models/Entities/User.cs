using System.ComponentModel.DataAnnotations;

namespace Auction.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

    }
}
