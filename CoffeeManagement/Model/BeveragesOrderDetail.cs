using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class BeveragesOrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        public virtual OrderDetail OrderDetail { get; set; }

        public Guid BeverageId { get; set; }
        [ForeignKey("BeverageId")]
        public virtual Beverage Beverage { get; set; }

        public int Quantity { get; set; } // Quantity of this specific beverage in the order
    }
}
