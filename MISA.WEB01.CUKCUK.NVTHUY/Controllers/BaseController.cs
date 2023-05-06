using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Properties

        private IBaseBL<T> _baseBL;

        #endregion

        #region Contructor
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Controller lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        /// Created By: NVTHUY(20/04/2023)
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
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Exception xử lý ngoại lệ
        /// </summary>
        /// <param name="ex">Ngoại lệ được bắt</param>
        /// <returns>Lỗi</returns>
        /// Created By: NVTHUY(20/04/2023)
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

        #endregion
    }
}
