using AutoMapper;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Interface;
using CoffeeManagement.ResponseModel.Shift;
using CoffeeManagement.Services.Interface;
using CoffeeManagement.Template;
using static CoffeeManagement.Constant.ApiEndPointConstant;
using static CoffeeManagement.Exceptions.ApiException;

namespace CoffeeManagement.Services.Implement
{
    public class ShiftService : BaseService<ShiftService>, IShiftService
    {
        public ShiftService(IUnitOfWork<DataBaseContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ShiftService> logger)
            : base(unitOfWork, mapper, httpContextAccessor, logger)
        {

        }

        public async Task<List<ShiftResponse>> GetAllShift()
        {
            try
            {
                var response = await _unitOfWork.GetRepository<Shift>().GetListAsync();

             return _mapper.Map<List<ShiftResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Shift list:{Message}",ex.Message);
                throw;  
            }
        }

        public async Task<ShiftResponse> GetShiftById(int shiftId)
        {
            try
            {
                var response = await _unitOfWork.GetRepository<Shift>().FirstOrDefaultAsync(
                    predicate:  s => s.ShiftId == shiftId
                    );
                if(response == null)
                {
                    throw new NotFoundException($"Shift with Id {shiftId} not found");
                }
                return _mapper.Map<ShiftResponse>(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving Shift :{Message}", ex.Message);
                throw;
            }
        }

    }
}
