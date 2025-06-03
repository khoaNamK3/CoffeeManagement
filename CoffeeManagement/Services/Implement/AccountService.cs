using AutoMapper;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Interface;
using CoffeeManagement.RequestModel.Account;
using CoffeeManagement.ResponseModel.Account;
using CoffeeManagement.Services.Interface;
using CoffeeManagement.Template;
using CoffeeManagement.Validations;
using System.Net;
using System.Numerics;
using static CoffeeManagement.Exceptions.ApiException;
using static CoffeeManagement.Model.Role;


namespace CoffeeManagement.Services.Implement
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        private readonly AccountValidation _validationRules;
        public AccountService(IUnitOfWork<DataBaseContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<AccountService> logger, AccountValidation validationRules)
         : base(unitOfWork, mapper, httpContextAccessor, logger)
        {
            _validationRules = validationRules;
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAccount()
        {
            try
            {
                var accountList = await _unitOfWork.GetRepository<Account>().GetListAsync();

                var responseList = accountList.Select(account => new AccountResponse
                {
                   
                    Name = account.Name,
                    Email = account.Email,
                   Password = account.Password,
                    Phone = account.Phone,
                    Address = account.Address,
                    IsActive = account.IsActive,
                    RoleId = account.RoleId.ToString(),
                }).ToList();

                return responseList;
                // AccountResponse ko the khoi tao type roleType => ko dung duoc outo mapper
                //var repsonse = _mapper.Map<AccountResponse>(AccountList);   
                //return  _mapper.Map<List<AccountResponse>>(response);          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Account list:{Message}", ex.Message);
                throw;
            }
        } 

        public async Task<AccountResponse> GetAccountById(Guid accountId)
        {
            try
            {
                var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(
                    predicate: a => a.AccountId == accountId
                    );
                var response = new AccountResponse
                {
                    Name = account.Name,
                    Email = account.Email,
                    Password = account.Password,
                    Phone = account.Phone,
                    Address = account.Address,
                    IsActive = account.IsActive,
                    RoleId = account.RoleId.ToString(),
                };
                if(response == null)
                {
                    throw new NotFoundException($"Account with Id {accountId} not found");
                }
                return _mapper.Map<AccountResponse>(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving Account :{Message}", ex.Message);
                throw;
            }
        }

        public async Task<AccountResponse> CreateNewAccount(AccountRequest newAccountRequest, Role.RoleType roleType)
        {
            try
            {
                var validationResult = _validationRules.Validate(newAccountRequest);
                if (!validationResult.IsValid) {
                    throw new ValidationException(validationResult.Errors);
                }
               
                var newAccount = _mapper.Map<Account>(newAccountRequest);
                newAccount.Role = await _unitOfWork.GetRepository<Role>().GetByIdAsync(roleType);
                    newAccount.RoleId = roleType;
                await _unitOfWork.ExcuteInTransactionAsync(async () =>
                {
                    await _unitOfWork.GetRepository<Account>().InsertAsync(newAccount);
                    return newAccount;
                });

                string roleName = newAccount.RoleId.ToString();
                var accountRepsone =   _mapper.Map<AccountResponse>(newAccount);
                accountRepsone.RoleId = roleName;
                return accountRepsone;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, " Error to Create Account :{Message}", ex.Message);
                throw;
            }
        } 

       public async Task<AccountResponse> UpdateAccountById(Guid id, AccountRequest updateAccountRequest)
        {
            try
            {
                var validationResult = _validationRules.Validate(updateAccountRequest);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return await _unitOfWork.ExcuteInTransactionAsync(async () =>
                {
                    var existingAccount = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(predicate: a => a.AccountId == id);

                    _mapper.Map(updateAccountRequest, existingAccount);

                    _unitOfWork.GetRepository<Account>().UpdateAsync(existingAccount);

                    var response = new AccountResponse
                    {
                        Name = existingAccount.Name,
                        Email = existingAccount.Email,
                        Password = existingAccount.Password,
                        Phone = existingAccount.Phone,
                        Address = existingAccount.Address,
                        IsActive = existingAccount.IsActive,
                        RoleId = existingAccount.RoleId.ToString(),
                    };
                    return response;
                });
            }catch(Exception ex)
            {
                _logger.LogError(ex, " Error to Create Account :{Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAccout(Guid id)
        {
            try
            {
                return await _unitOfWork.ExcuteInTransactionAsync( async () =>
                {
                    var existingAccount = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(
                        predicate: a => a.AccountId == id);
                        
                    if(existingAccount == null)
                    {
                        throw new NotFoundException($"Account with Id {id} not found");
                    }
                     _unitOfWork.GetRepository<Account>().DeleteAsync(existingAccount);
                    return true;
                });
            }catch(Exception ex)
            {
                _logger.LogError(ex, " Error to Create Account :{Message}", ex.Message);
                throw;
            }
        }
    }
}
