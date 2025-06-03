using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagement.Model
{
    public class Shift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShiftId { get; set; }

        
        public DateTime WorkingDate { get; set; }  

        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }  

       
        public TimeOnly StartTime { get; set; } 

        public TimeOnly EndTime { get; set; } 

        // Many-to-Many
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        // Ensure EndTime is after StartTime
        public bool IsValidShift()
        {
            return EndTime > StartTime;
        }
    }
}
