using AutoMapper;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Interface;
using CoffeeManagement.RequestModel.Shift;
using CoffeeManagement.ResponseModel.Shift;
using CoffeeManagement.Services.Interface;
using CoffeeManagement.Template;
using CoffeeManagement.Validations;
using static CoffeeManagement.Constant.ApiEndPointConstant;
using static CoffeeManagement.Exceptions.ApiException;

namespace CoffeeManagement.Services.Implement
{
    public class ShiftService : BaseService<ShiftService>, IShiftService
    {
        private readonly ShiftValidation _validationRules;

        public ShiftService(IUnitOfWork<DataBaseContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ShiftService> logger, ShiftValidation validationRules)
            : base(unitOfWork, mapper, httpContextAccessor, logger)
        {
            _validationRules = validationRules;
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

        public async Task<ShiftResponse> GetShiftById(Guid shiftId)
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

        public async Task<ShiftResponse> CreateNewShift(ShiftRequest newShiftRequest)
        {
            try
            {      
                var validationResult = _validationRules.Validate(newShiftRequest);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var newShift = _mapper.Map<Shift>(newShiftRequest);

                await _unitOfWork.ExcuteInTransactionAsync(async () =>
                {
                    await _unitOfWork.GetRepository<Shift>().InsertAsync(newShift);

                    return newShift;
                });
                return _mapper.Map<ShiftResponse>(newShift);
            }
            catch (Exception ex) {
            _logger.LogError(ex,"Erro create Shift {Message}", ex.Message);
                throw;
            }
        }


        public async Task<ShiftResponse> UpdateShiftById(Guid id, ShiftRequest updateShiftRequest)
        {
            try
            {
                // validate the request
                var validationResult = _validationRules.Validate(updateShiftRequest);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                // use to re
                return await _unitOfWork.ExcuteInTransactionAsync(async () =>
                {
                 // get shift Id
                 var existingShift = await _unitOfWork.GetRepository<Shift>().FirstOrDefaultAsync(predicate: s => s.ShiftId == id);

                 // Mapper Shift properties
                 _mapper.Map(updateShiftRequest, existingShift);

                 // Update shift
                 _unitOfWork.GetRepository<Shift>().UpdateAsync(existingShift);

                 var response =  _mapper.Map<ShiftResponse>(existingShift);
                 return response;

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shift: {Message}", ex.Message);
                throw;
            }
        }
    
        public async Task<bool> DeleteShift(Guid id)
        {
            try
            {
                return await _unitOfWork.ExcuteInTransactionAsync(async () =>
                {
                    var exittingShift = await _unitOfWork.GetRepository<Shift>().FirstOrDefaultAsync(
                        predicate: s => s.ShiftId == id
                    );
                    if (exittingShift == null) {
                        throw new NotFoundException($"Shift with Id {id} not found");
                    }
                       _unitOfWork.GetRepository<Shift>().DeleteAsync(exittingShift);
                    return true;
                });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error delete shift: {Message}", ex.Message);
                throw;
            }
        }
    }
}
