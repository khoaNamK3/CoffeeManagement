using CoffeeManagement.RequestModel.Shift;
using CoffeeManagement.ResponseModel.Shift;

namespace CoffeeManagement.Services.Interface
{
    public interface IShiftService
    {
        public Task<List<ShiftResponse>> GetAllShift();

        public Task<ShiftResponse> GetShiftById(Guid shiftId);

        public Task<ShiftResponse> CreateNewShift(ShiftRequest newShiftRequest);

        public  Task<ShiftResponse> UpdateShiftById(Guid id, ShiftRequest updateShiftRequest);

        public  Task<bool> DeleteShift(Guid id);
    }
}
