namespace CoffeeManagement.ResponseModel.Shift
{
    public class ShiftResponse
    {
        public DateTime WorkingDate { get; set; }

        public decimal Salary { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
