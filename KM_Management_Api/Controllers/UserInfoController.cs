using KM_Management_Api.Models;
using KM_Management_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _userInfoService.GetAll();
                return Ok(new { isSuccess = true, data = res });
                //return await _problemsService.GetAll();
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    StatusCode = 500,
                    message = ex.Message

                });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var res = await _userInfoService.GetById(id);
                return Ok(new { isSuccess = true, data = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    StatusCode = 500,
                    message = ex.Message

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserInfo userInfo)
        {
            try
            {
                var res = await _userInfoService.Add(userInfo);
                return Ok(new { isSuccess = true, data = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    StatusCode = 500,
                    message = ex.Message

                });
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromBody] UserInfo userInfo)
        {
            try
            {
                var res = await _userInfoService.Update(userInfo);
                return Ok(new { isSuccess = true, data = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    StatusCode = 500,
                    message = ex.Message

                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _userInfoService.Delete(id);
                return Ok(new { isSuccess = true, data = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    isSuccess = false,
                    StatusCode = 500,
                    message = ex.Message

                });
            }
        }
    }
}
