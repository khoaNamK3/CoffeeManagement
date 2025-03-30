using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagement.Model
{
    public class Role
    {
        public enum RoleType
        {
            Admin = 1,
            StaffManager = 2,
            Employee = 3,
            Customer = 4
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RoleType Id { get; set; }    
        public string Name => Id.ToString(); // get Role name
        public string Permissions { get; set; }

        // One Role can have many Accounts
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    }
}
