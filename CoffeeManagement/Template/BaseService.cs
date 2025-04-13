using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Interface;
using AutoMapper;
using System.Security.Claims;
namespace CoffeeManagement.Template
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<DataBaseContext> _unitOfWork;
        protected IMapper _mapper;
        protected ILogger<T> _logger;
        protected IHttpContextAccessor _contextAccessor;
        public BaseService(IUnitOfWork<DataBaseContext> unitOfWork,IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<T> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; // User libary to mapper the same field
            _contextAccessor = httpContextAccessor; // User to access HttpContext to get infomation 
            _logger = logger;
        }

        // get user Id in token 
        protected Guid GetCurrentUserId() {
            var userIdClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) ||  !Guid.TryParse(userIdClaim,out Guid userId)) {
                throw new UnauthorizedAccessException("User Id is not found in token");
            }
            return userId;
        }
    }
}
