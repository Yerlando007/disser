using Microsoft.AspNetCore.Mvc;
using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF.Users;

namespace disser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpPost("Register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] FormDataUser user)
        {
            Response<List<User>> result = new Response<List<User>>();
            try
            {
                List<User> res = await _userService.Register(user);

                if (res != null)
                {
                    result.StatusCode = 0;
                    result.Result = res;
                }
                else
                {
                    result.StatusCode = -2;
                    result.ErrorMessage = "Не найдено";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод CloseLoans()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }

            return Ok(result);
        }
    }
}
