using CoffeeManagement.Constant;
using CoffeeManagement.MetaData;
using CoffeeManagement.Model;
using CoffeeManagement.RequestModel.Account;
using CoffeeManagement.ResponseModel.Account;
using CoffeeManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CoffeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService): base(logger) 
        {
            _accountService = accountService;
        }

        // Get ALl 
        [HttpGet(ApiEndPointConstant.Account.AccountsEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountResponse>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status500InternalServerError)]
        //Authen
        public async Task<IActionResult> GetAllAccount()
        {
          var accounts = await  _accountService.GetAllAccount();
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Accounts list retrived Successfully",
                data: accounts,
                statusCode: StatusCodes.Status200OK
                ));
        }

        //Get By Id
        [HttpGet(ApiEndPointConstant.Account.GetAccountEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountResponse>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        //Authen
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var account = await _accountService.GetAccountById(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                message:"Get Account Successfully",
                data: account,
                statusCode: StatusCodes.Status200OK
                ));
        }

        // create new Account
        [HttpPost(ApiEndPointConstant.Account.CreateAccountEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<AccountResponse>),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        //Authen
        public async Task<IActionResult> CreateNewAccount([FromBody] AccountRequest newAccount,Role.RoleType roleType)
        {
          var response =  await _accountService.CreateNewAccount(newAccount, roleType);
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Create new Account Successfully",
                data: response,
                statusCode: StatusCodes.Status201Created
                ));
        }

        [HttpPatch(ApiEndPointConstant.Account.UpdateAccountEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        // Authen
        public async Task<IActionResult> UpdateAccount([FromBody]AccountRequest updateAccountRequest,[FromQuery]Guid id)
        {
            var response = await _accountService.UpdateAccountById(id,updateAccountRequest);
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Update Account Successfully",
                data: response,
                statusCode: StatusCodes.Status200OK
                ));
        }

        [HttpDelete(ApiEndPointConstant.Account.DeleteAccountEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        // Authen
        public async Task<IActionResult> DeleteAccount([FromQuery]Guid id)
        {
            var repsone = await _accountService.DeleteAccout(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Delete Account Successfully",
                data: repsone,
                statusCode: StatusCodes.Status200OK
                ));
        }
    }
}
