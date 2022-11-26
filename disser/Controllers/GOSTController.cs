using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace disser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GOSTController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        private readonly IGOST _igost;
        public GOSTController(ILogger<UserController> logger, IUserService userService, AppDbContext db, IGOST gost)
        {
            _logger = logger;
            _userService = userService;
            _igost = gost;
            _db = db;
        }
        [HttpPost("CreteGOST")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateGOST([FromForm] GostFormData gost)
        {
            Response<List<GOST>> result = new Response<List<GOST>>();
            try
            {
                List<GOST> res = await _igost.CreateGOST(User.Identity.Name, gost);

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
            ////var role = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            //if (User.Identity.IsAuthenticated && role.Role == "Создатель")
            //{

            //}
            //return BadRequest("Пользователь не имеет доступа");
        }
    }
}
