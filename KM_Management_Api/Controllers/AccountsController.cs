using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using KM_Management_Api.Models;
using KM_Management_Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAccountService accountService;
        public AccountsController(IAccountService accountService
            , IConfiguration configuration)
        {
            this.accountService = accountService;
            this.configuration = configuration;
        }

        [HttpPost("gettokenlogin")]
        public async Task<IActionResult> GetTokenLogin(GetTokenRequest getTokenRequest)
        {
            try
            {
                var res = await accountService.GetTokenLogin(getTokenRequest);
                return Ok(new { isSuccess = true, data = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }
    }
}
