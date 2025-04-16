using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagement.Controllers
{
    public class BaseController<T>: ControllerBase where T : BaseController<T>
    {
        protected ILogger<T> _logger;

        public BaseController(ILogger<T> logger) {
        _logger = logger;
        }
    }
}
