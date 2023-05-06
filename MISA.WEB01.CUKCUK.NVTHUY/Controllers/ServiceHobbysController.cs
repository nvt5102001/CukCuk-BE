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
    public class ServiceHobbysController : BaseController<ServiceHobby>
    {
        #region Properties

        private IServiceHobbyBL _serviceHobbyBL;

        #endregion

        #region Properties

        public ServiceHobbysController(IServiceHobbyBL serviceHobbyBL) : base(serviceHobbyBL)
        {
            _serviceHobbyBL = serviceHobbyBL;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Controller lấy ra danh sách phục thêm theo ID món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <returns>Danh sách phục vụ thêm theo ID</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (24/04/2023)
        [HttpGet("list-service-byid")]
        public IActionResult GetListService(Guid foodID)
        {
            try
            {
                if (!Guid.Equals(foodID, Guid.Empty))
                {
                    var res = _serviceHobbyBL.GetListService(foodID);
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.NullData);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion

    }
}
