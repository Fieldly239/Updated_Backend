using KM_Management_Api.Models;
using KM_Management_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KMController : ControllerBase
    {
        private readonly IKMService _kmService;

        public KMController(IKMService kmService)
        {
            _kmService = kmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _kmService.GetAll();
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

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _kmService.GetById(id);
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

        [HttpGet]
        [Route("getbyapplication/{id}")]
        public async Task<IActionResult> GetByApplication(int id)
        {
            try
            {
                var res = await _kmService.GetByApplication(id);
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

        [HttpGet]
        [Route("getbycategory/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            try
            {
                var res = await _kmService.GetByCategory(id);
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

        [HttpGet]
        [Route("getbystatus/{id}")]
        public async Task<IActionResult> GetByStatus(int id)
        {
            try
            {
                var res = await _kmService.GetByStatus(id);
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] KM model)
        {
            try
            {
                var res = await _kmService.Add(model);
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


        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromBody] KM model)
        {
            try
            {
                var res = await _kmService.Update(model);
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

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _kmService.Delete(id);
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

        [HttpPost]
        [Route("searchall")]
        public async Task<IActionResult> GetSearchAll([FromBody] KMSearchAll model)
        {
            try
            {
                var res = await _kmService.GetSearchAll(model);
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

        [HttpGet]
        [Route("gettopknowledge")]
        public async Task<IActionResult> GetTopKnowLedge()
        {
            try
            {
                var res = await _kmService.GetTopKnowLedge();
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
