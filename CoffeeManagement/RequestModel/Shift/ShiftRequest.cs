
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace CoffeeManagement.RequestModel.Shift
{
    public class ShiftRequest
    {

        public DateTime WorkingDate { get; set; }

       
        public decimal Salary { get; set; }

       
        public TimeOnly StartTime { get; set; }

        
        public TimeOnly EndTime { get; set; }

      
    }
}

