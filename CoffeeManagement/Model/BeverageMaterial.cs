using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class BeverageMaterial
    {
        [Key]
        public Guid BeverageId { get; set; }
        [ForeignKey("BeverageId")]
        public virtual Beverage Beverage { get; set; }

        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal QuantityUsed { get; set; } // Quantity of material used for the beverage
    }
}
