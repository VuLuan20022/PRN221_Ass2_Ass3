using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class Category
    {

        [Key]

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }

    }
}
