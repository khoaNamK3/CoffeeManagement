using CoffeeManagement.Model;
using CoffeeManagement.RequestModel.Account;
using CoffeeManagement.ResponseModel.Account;

namespace CoffeeManagement.Services.Interface
{
    public interface IAccountService
    {
        public Task<IEnumerable<AccountResponse>> GetAllAccount();
        public Task<AccountResponse> GetAccountById(Guid accountId);
        public Task<AccountResponse> CreateNewAccount(AccountRequest newAccountRequest,Role.RoleType roleType);
        public  Task<AccountResponse> UpdateAccountById(Guid id, AccountRequest updateAccountRequest);
        public  Task<bool> DeleteAccout(Guid id);
    }
}
