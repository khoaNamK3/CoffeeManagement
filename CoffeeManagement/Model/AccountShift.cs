namespace CoffeeManagement.Model
{
    public class AccountShift
    {
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int ShiftId { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
