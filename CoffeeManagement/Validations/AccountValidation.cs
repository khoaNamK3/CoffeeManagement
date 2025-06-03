using CoffeeManagement.RequestModel.Account;
using FluentValidation;

namespace CoffeeManagement.Validations
{
    public class AccountValidation :AbstractValidator<AccountRequest>
    {
           public AccountValidation() {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name of Account can't empty");
            RuleFor(a => a.Email).NotEmpty().WithMessage("Email of account can't empty");
            RuleFor(a => a.Password).NotEmpty().WithMessage("PassWord of account can't empty");
            RuleFor(a => a.Address).NotEmpty().WithMessage("Address of account can't empty");
            RuleFor(a => a.Phone).NotEmpty().WithMessage("Phone of account can't empty")
           .Matches(@"^0\d{9}$").WithMessage("Phone number must be 9 digits and start with 0");                       
            }
    }
}
