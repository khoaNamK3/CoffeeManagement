using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagement.Model
{
    public class Beverage
    {
        public enum BeverageType
        {
            Coffee,
            Tea,
            Juice,
            SoftDrink,
            Alcohol,
            Other
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BeverageId { get; set; }
        public string BeverageName { get; set; }
        public string Desciption { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public BeverageType Type { get; set; }

        // Many-to-Many: Beverage ↔ Material
        public virtual ICollection<Material>Materials { get; set; } = new List<Material>();
    }
}

