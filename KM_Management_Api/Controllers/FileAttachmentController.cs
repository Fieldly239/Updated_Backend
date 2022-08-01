using KM_Management_Api.Models;
using KM_Management_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KM_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileAttachmentController : ControllerBase
    {
        private readonly IFileAttachmentService fileAttachmentService;
        public FileAttachmentController(IFileAttachmentService fileAttachmentService)
        {
            this.fileAttachmentService = fileAttachmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await fileAttachmentService.GetAll();
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
                var res = await fileAttachmentService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] FileAttachment model)
        {
            try
            {
                var res = await fileAttachmentService.Add(model);
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
        public async Task<IActionResult> Update([FromBody] FileAttachment model)
        {
            try
            {
                var res = await fileAttachmentService.Update(model);
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
                var res = await fileAttachmentService.Delete(id);
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

        [HttpPost("fileupload")]
        public async Task<IActionResult> FileUpload([FromForm] FileAttachment files)
        {
            try
            {
                long size = files.FormFiles.Sum(f => f.Length);
                var filePaths = await fileAttachmentService.FileUpload(files);

                return Ok(new { isSuccess = true, count = files.FormFiles.Count, size, filePaths });
            }
            catch (Exception ex)
            {
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }

        }

        [HttpGet("getfileinfo/{id}")]
        public async Task<IActionResult> GetFileInfo(int id)
        {
            try
            {
                var _file = await fileAttachmentService.GetFileInfo(id);
                return new JsonResult(new { isSuccess = true, data = _file });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpGet("getallbykmid")]
        public async Task<IActionResult> GetAllByKMId(string kmid)
        {
            try
            {
                var result = await fileAttachmentService.GetAllByKMId(kmid);
                return new JsonResult(new { isSuccess = true, data = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
