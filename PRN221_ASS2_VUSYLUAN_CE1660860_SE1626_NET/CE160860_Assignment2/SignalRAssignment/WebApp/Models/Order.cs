using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipAddress { get; set; } = null!;


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
