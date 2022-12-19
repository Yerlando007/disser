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
        [HttpPost("AddGOSTS")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddGOSTS()
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Создатель")
            {
                var result = new Response<List<AllGOST>>();
                try
                {
                    var res = await _igost.AddGOSTS();

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
        [HttpPost("IspolnitelWork")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> IspolnitelWork([FromForm] IspolnitelWork ispolnitelWork)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Исполнитель")
            {
                var result = new Response<RukovoditelWantWork>();
                try
                {
                    var res = await _igost.IspolnitelWork(ispolnitelWork, userRequest.Username);

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
        [HttpPost("AccepTranslatorWork")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AccepTranslatorWork([FromForm] AcceptTanslatorWork acceptTanslatorWork)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Руководитель")
            {
                var result = new Response<TranslateFile>();
                try
                {
                    var res = await _igost.AccepTranslatorWork(acceptTanslatorWork);

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
        [HttpPost("TranslatorAddFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> TranslatorAddFile([FromForm] TranslatorAddFile translatorAddFile)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Переводчик")
            {
                var result = new Response<List<TranslateFile>>();
                try
                {
                    var res = await _igost.TranslatorAddFile(translatorAddFile, userRequest.Username);

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
        [HttpPost("RukovoditelAcceptWork")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RukovoditelAcceptWork([FromForm] RukovoditelAcceptWork rukovoditelAcceptWork)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Руководитель")
            {
                var result = new Response<CreatedGOST>();
                try
                {
                    var res = await _igost.RukovoditelAcceptWork(rukovoditelAcceptWork);

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
        [HttpPost("ChoiseRukovoditel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ChoiseRukovoditel([FromForm] ChoisedRukovoditel choisedRukovoditel)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Создатель")
            {
                var result = new Response<CreatedGOST>();
                try
                {
                    var res = await _igost.ChoiseRukovoditel(choisedRukovoditel);

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
                    _logger.LogError(ex, "Ошибка метод ChoiseRukovoditel()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }
                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }
        [HttpPost("GiveTasktoIspolnitel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GiveTasktoIspolnitel([FromForm] GiveTasktoIspolnitel giveTasktoIspolnitel)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Руководитель")
            {
                var result = new Response<RukovoditelWantWork>();
                try
                {
                    var res = await _igost.GiveTasktoIspolnitel(giveTasktoIspolnitel);

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
                    _logger.LogError(ex, "Ошибка метод ChoiseRukovoditel()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }
                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }
        [HttpPost("TakeGOSTsByRukovoditel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> TakeGOSTsByRukovoditel([FromForm] TakeGOSTs takeGosts)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Руководитель")
            {
                var result = new Response<List<RukovoditelWantWork>>();
                try
                {
                    var res = await _igost.TakeGOSTsByRukovoditel(takeGosts, userRequest.Username);

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
                    _logger.LogError(ex, "Ошибка метод TakeGOSTs()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }
                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }
        [HttpPost("CreateGOST")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateGOST([FromForm] GostFormData gost)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated && userRequest.Role == "Создатель")
            {
                var result = new Response<List<CreatedGOST>>();
                try
                {
                    var res = await _igost.CreateGOST(gost, userRequest.Username);

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
                    _logger.LogError(ex, "Ошибка метод CreateGOST()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }
        [HttpGet("GetCreatedGOST")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetCreatedGOST(int id)
        {          
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                var result = new Response<CreatedGOST>();
                try
                {
                    var res = await _igost.GetCreatedGOST(id);

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
                    _logger.LogError(ex, "Ошибка метод CreateGOST()");
                    result.StatusCode = -1;
                    result.ErrorMessage = ex.Message.ToString();
                }

                return Ok(result);
            }
            return BadRequest("Пользователь не имеет доступа");
        }
    }
}
