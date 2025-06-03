using AutoMapper;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Implement;
using CoffeeManagement.Repositories.Interface;
using CoffeeManagement.ResponseModel.Role;
using CoffeeManagement.Services.Interface;
using CoffeeManagement.Template;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static CoffeeManagement.Constant.ApiEndPointConstant;
using static CoffeeManagement.Exceptions.ApiException;


namespace CoffeeManagement.Services.Implement
{
    public class RoleService :BaseService<RoleService>,IRoleService
    {
       
       public RoleService(IUnitOfWork<DataBaseContext> unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor,ILogger<RoleService> logger)
            :base(unitOfWork,mapper, httpContextAccessor,logger)
        {
        
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRole()
        {
            try
            {
              
               var response = await _unitOfWork.GetRepository<Role>().GetListAsync();
               
                return _mapper.Map<IEnumerable<RoleResponse>>(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving Roles list:{Message}", ex.Message);
                throw;
            }
        }

        public async Task<RoleResponse> GetRoleById(Role.RoleType roleId)
        {
            try
            {
                var response = await _unitOfWork.GetRepository<Role>().FirstOrDefaultAsync(
                    predicate: r => r.Id == roleId
                );


                if (response == null)
                {
                    throw new NotFoundException($"Role with Id {roleId} not found");
                }
                return _mapper.Map<RoleResponse>(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving Role :{Message}", ex.Message);
                throw;
            }

        }

    }
}
