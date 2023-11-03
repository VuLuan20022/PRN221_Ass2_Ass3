using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }


    }
}
