using CoffeeManagement.Constant;
using CoffeeManagement.MetaData;
using CoffeeManagement.ResponseModel.Shift;
using CoffeeManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : BaseController<ShiftController>
    {
        private readonly IShiftService _shiftService;

        public ShiftController(ILogger<ShiftController> logger,IShiftService shiftService) :base(logger)
        {
        _shiftService = shiftService;
        }
        // GetAll
        [HttpGet(ApiEndPointConstant.shift.ShiftsEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ShiftResponse>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        // Authen
        public async Task<IActionResult> GetAllShifts()
        {
            var Shifts = await _shiftService.GetAllShift();
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK, "Shifts list retrived Successfully", Shifts
                ));
        }

        //Get By Id
        [HttpGet(ApiEndPointConstant.shift.GetShiftEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ShiftResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        // Authen
        public async Task<IActionResult> GetShiftById([FromRoute] int id)
        {
            var shift = await _shiftService.GetShiftById(id);
            return Ok(ApiResponseBuilder.BuildResponse(StatusCodes.Status200OK, "Get Shift Successfully", shift
                ));
        }
    }
}
