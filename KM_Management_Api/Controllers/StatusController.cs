using KM_Management_Api.Models;
using KM_Management_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _statusService.GetAll();
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
                var res = await _statusService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Status status)
        {
            try
            {
                var res = await _statusService.Add(status);
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
        public async Task<IActionResult> Update([FromBody] Status status)
        {
            try
            {
                var res = await _statusService.Update(status);
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
                var res = await _statusService.Delete(id);
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
