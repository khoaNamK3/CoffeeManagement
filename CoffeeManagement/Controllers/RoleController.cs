using CoffeeManagement.Constant;
using CoffeeManagement.MetaData;
using CoffeeManagement.ResponseModel.Role;
using CoffeeManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data.SqlTypes;

namespace CoffeeManagement.Controllers
{
    public class RoleController :BaseController<RoleController>
    {
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger,IRoleService roleService):base(logger)
        { 
        _roleService = roleService;
        }

        //GetAll
        [HttpGet(ApiEndPointConstant.role.RolesEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleResponse>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status500InternalServerError)]
        // Authentication
        public async Task<IActionResult> GetAllRole()
        {
            var userClaims = User.Claims.Select(x => new {x.Type,x.Value});
            var roles = await _roleService.GetAllRole();
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Role list retrived Successfully",
                data: roles,
                statusCode: StatusCodes.Status200OK
                ));
        }

        //GetById
        [HttpGet(ApiEndPointConstant.role.GetRoleEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleResponse>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        //Authentication
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Role = await _roleService.GetRoleById(id);
               return Ok(ApiResponseBuilder.BuildResponse(
               message: "Get Role  Successfully",
               data: Role,
               statusCode: StatusCodes.Status200OK
                ));

        }
    }
}
