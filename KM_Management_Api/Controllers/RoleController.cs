using KM_Management_Api.Models;
using KM_Management_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _roleService.GetAll();
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
                var res = await _roleService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Role role)
        {
            try
            {
                var res = await _roleService.Add(role);
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
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            try
            {
                var res = await _roleService.Update(role);
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
                var res = await _roleService.Delete(id);
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
