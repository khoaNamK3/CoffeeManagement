using CoffeeManagement.ResponseModel.Shift;

namespace CoffeeManagement.Services.Interface
{
    public interface IShiftService
    {
        public Task<List<ShiftResponse>> GetAllShift();

        public Task<ShiftResponse> GetShiftById(int shiftId);
    }
}
