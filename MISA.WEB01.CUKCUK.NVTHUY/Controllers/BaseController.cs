using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IBaseBL<T> _baseBL;

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _baseBL.GetAll();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Exception xử lý ngoại lệ
        /// </summary>
        /// <param name="ex">Ngoại lệ được bắt</param>
        /// <returns></returns>
        /// CreatedBy NVThuy 28/08/2022
        protected IActionResult HandleException(dynamic ex)
        {
            var error = new
            {
                devMsg = ex.Message,
                userMsg = Resources.ResourceManager.GetString(name: "ErrorException"),
                errorMsg = ex.Data["Error"]
            };

            if (ex is ErrorException)
            {
                return BadRequest(error);
            }
            return StatusCode(500, error);
        }
    }
}
