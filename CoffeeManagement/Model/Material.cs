using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialType { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime ExpiryDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        public int ShopPhone { get; set; }

        // Many-to-Many: Material ↔ Beverage
        public virtual ICollection<BeverageMaterial> BeverageMaterials { get; set; } = new List<BeverageMaterial>();
    }
}
