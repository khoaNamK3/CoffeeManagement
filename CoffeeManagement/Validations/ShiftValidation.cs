using CoffeeManagement.RequestModel.Shift;
using FluentValidation;

namespace CoffeeManagement.Validations
{
    public class ShiftValidation : AbstractValidator<ShiftRequest>
    {
        public ShiftValidation() {
            RuleFor(s => s.WorkingDate)
           .NotEmpty().WithMessage("Working Date can not be null or empty")
           .Must(date => date.Date >= DateTime.Today)
           .WithMessage("Working Date must be today or in the future");

            RuleFor(s => s.Salary)
             .NotNull().WithMessage("Salary can not be null")
             .GreaterThanOrEqualTo(0).WithMessage("Must be greater than  or equal 0");

            var minTime = new TimeOnly(8, 0);   // 07:00 AM
            var maxTime = new TimeOnly(22, 0);  // 10:00 PM


            RuleFor(s => s.StartTime)
               .Must(start => start >= minTime && start <= maxTime).WithMessage("StartTime must be between 07:00 and 22:00");

            RuleFor(x => x.EndTime)
                .Must(end => end >= minTime && end <= maxTime).WithMessage("EndTime must be between 07:00 and 22:00");

            RuleFor(s => s)
                .Must(s => s.EndTime > s.StartTime).WithMessage("EndTime must be greater than StartTime");
        }
    }
}
