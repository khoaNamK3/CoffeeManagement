using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagement.Model
{
    public class Shift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShiftId { get; set; }

        
        public DateTime WorkingDate { get; set; }  

        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }  

       
        public TimeSpan StartTime { get; set; } 

        public TimeSpan EndTime { get; set; } 

        // Many-to-Many
        public virtual ICollection<AccountShift> AccountShifts { get; set; } = new List<AccountShift>();

        // Ensure EndTime is after StartTime
        public bool IsValidShift()
        {
            return EndTime > StartTime;
        }
    }
}
