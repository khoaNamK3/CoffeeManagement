namespace CoffeeManagement.Model
{
    public class AccountShift
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid ShiftId { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
