using Microsoft.AspNetCore.Mvc;
using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF.Users;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace disser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _db;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService, AppDbContext db)
        {
            _logger = logger;
            _userService = userService;
            _db = db;
        }

        [HttpGet("GetLogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [HttpGet("GetUserInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && role.Role == "Admin")
            {
                var result = new Response<List<User>>();
                try
                {
                    List<User> res = await _userService.GetUserInfo(id);

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
                    _logger.LogError(ex, "Ошибка метод GetUserInfo()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }

        [HttpPost("VerifyUser")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> VerifyUser([FromForm] VerifyFormData verify)
        {
            var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && role.Role == "Admin")
            {
                var result = new Response<List<User>>();
                try
                {
                    List<User> res = await _userService.VerifyUser(verify);

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
                    _logger.LogError(ex, "Ошибка метод VerifyUser()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }

        [HttpGet("GetUsers")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetUsers()
        {
            var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && role.Role == "Admin")
            {
                var result = new Response<List<User>>();
                try
                {
                    List<User> res = await _userService.GetUsers();

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
                    _logger.LogError(ex, "Ошибка метод GetUsers()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }

        [HttpGet("GetIspoltinel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetIspoltinel()
        {
            var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && role.Role == "Руководитель")
            {
                var result = new Response<List<User>>();
                try
                {
                    List<User> res = await _userService.GetIspoltinel(role.Id);

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
                    _logger.LogError(ex, "Ошибка метод GetIspoltinel()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }

        [HttpGet("GetRukovoditel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetRukovoditel()
        {
            var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && role.Role == "Создатель")
            {
                var result = new Response<List<User>>();
                try
                {
                    var res = await _userService.GetRukovoditel();

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
                    _logger.LogError(ex, "Ошибка метод GetRukovoditel()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }

        [HttpPost("Login")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Login([FromForm] LoginFormData user)
        {
            var result = new Response<LoginRole<List<AuthOptions>>>();
            try
            {
                var res = await _userService.GetIdentity(user);

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
                _logger.LogError(ex, "Ошибка метод Login()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }


        [HttpPost("Register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] UserFormData user)

        {
            var result = new Response<List<User>>();
            try
            {
                var res = await _userService.Register(user);

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
                _logger.LogError(ex, "Ошибка метод Register()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }

            return Ok(result);
        }
    }
}
