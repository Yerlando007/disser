using Microsoft.AspNetCore.Mvc;
using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF;

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


        [HttpGet("Register")]
        public async Task<IActionResult> Register(string userName, string newUserName, string FIO, string? filial)
        {
            Response<List<User>> result = new Response<List<User>>();
            try
            {
                List<User> res = await _userService.Register(userName, newUserName, FIO, filial);

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
