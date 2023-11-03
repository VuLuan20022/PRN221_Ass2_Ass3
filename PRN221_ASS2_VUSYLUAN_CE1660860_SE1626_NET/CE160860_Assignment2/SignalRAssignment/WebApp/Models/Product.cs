using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class Product
    {

        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; }
        public Supplier SuppliersId { get; set; }
        public int SupplierId { get; set; }
        public Category CategoriesId { get; set; }
        public int CategoryId { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ProductImage { get; set; }
        public bool IsPizzaOfWeek { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
