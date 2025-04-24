using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagement.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        // one Role have many account
        [ForeignKey("Role")]
        public Role.RoleType RoleId { get; set; }
        public virtual Role Role { get; set; }

        // many to many
        public virtual ICollection<AccountShift> AccountShifts { get; set; } = new List<AccountShift>();

        // One-to-Many: One Account has many Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
