using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        // One-to-Many: Order → OrderDetail
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Many-to-Many: OrderDetail ↔ Beverage
        public virtual ICollection<BeveragesOrderDetail> BeveragesOrders { get; set; } = new List<BeveragesOrderDetail>();

    }
}
