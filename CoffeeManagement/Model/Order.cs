using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class Order
    {
        public enum OrderStatus
        {
            Pending =1 ,
            Complete =2 ,
            Cancel =3 
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; } // One-to-Many: Account to Orders
        public string Name { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

    }
}
