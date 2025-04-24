using Azure;
using CoffeeManagement.Constant;
using CoffeeManagement.MetaData;
using CoffeeManagement.RequestModel.Shift;
using CoffeeManagement.ResponseModel.Shift;
using CoffeeManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
               message: "Shifts list retrived Successfully",
               data: Shifts,
               statusCode: StatusCodes.Status200OK
                ));
        }

        //Get By Id
        [HttpGet(ApiEndPointConstant.shift.GetShiftEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ShiftResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        // Authen
        public async Task<IActionResult> GetShiftById([FromRoute] Guid id)
        {
            var shift = await _shiftService.GetShiftById(id);
            return Ok(ApiResponseBuilder.BuildResponse(
               message: "Get Shift Successfully",
               data: shift,
               statusCode: StatusCodes.Status200OK
                ));
        }

        // Create New Shift
        [HttpPost(ApiEndPointConstant.shift.CreateShiftEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<ShiftResponse>),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status422UnprocessableEntity)]
        // Authen
        public async Task<IActionResult> CreateNewShift([FromBody]ShiftRequest shiftRequest)
        {
            var response = await _shiftService.CreateNewShift(shiftRequest);
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Create new Shift successfully",
                data: response, 
                statusCode: StatusCodes.Status201Created
                ));
        }

        // Update Exits Shift 
        [HttpPatch(ApiEndPointConstant.shift.UpdateShiftEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<ShiftResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        // Authen

        public async Task<IActionResult> UpdateShift(Guid id, [FromBody]ShiftRequest updateShiftRequest)
        {
            var response = await _shiftService.UpdateShiftById(id, updateShiftRequest);
            return Ok(ApiResponseBuilder.BuildResponse(
                message: "Shift update successfully",
                data: response,
                statusCode: StatusCodes.Status200OK
                ));
        }

        // Delete Exits shift 
        [HttpDelete(ApiEndPointConstant.shift.DeleteShiftEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<ShiftResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteShift(Guid id)
        {
            await _shiftService.DeleteShift(id);
            return Ok(ApiResponseBuilder.BuildResponse(
               message: "Shift update successfully",
               data:Empty,
               statusCode: StatusCodes.Status200OK
               ));
        }
    }
}
