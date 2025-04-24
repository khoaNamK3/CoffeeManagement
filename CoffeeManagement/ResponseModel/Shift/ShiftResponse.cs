namespace CoffeeManagement.ResponseModel.Shift
{
    public class ShiftResponse
    {
        public DateTime WorkingDate { get; set; }

        public decimal Salary { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
