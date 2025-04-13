using AutoMapper;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Implement;
using CoffeeManagement.Template;

namespace CoffeeManagement.Services.Implement
{
    public class RoleService :BaseService<RoleService>
    {
       
       public RoleService(UnitOfWork<DataBaseContext> unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor,ILogger<RoleService> logger)
            :base(unitOfWork,mapper, httpContextAccessor,logger)
        {
        
        }

        public async Task<List<Role>> GetAllRole()
        {
            try
            {
               var response = await _unitOfWork.GetRepository<Role>().GetListAsync();
                return response.ToList();
            }
            catch (Exception ex) {
                throw;
            }
        }

    }
}
