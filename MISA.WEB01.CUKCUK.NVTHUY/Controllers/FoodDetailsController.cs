using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodDetailsController : BaseController<FoodDetail>
    {
        #region Properties

        private IFoodDetailBL _foodDetailBL;

        #endregion

        #region Contructor

        public FoodDetailsController(IFoodDetailBL foodDetailBL) : base(foodDetailBL)
        {
            _foodDetailBL = foodDetailBL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Controller thêm món ăn và sở thích phục vụ thêm
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>ID món ăn đã thêm</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (24/04/2023)
        [HttpPost]
        public IActionResult InsertFood([FromBody] FoodDetail foodDetail)
        {
            try
            {
                if(foodDetail != null)
                {
                    var result = _foodDetailBL.InsertFoodDetail(foodDetail);
                    return StatusCode((int)result.StatusCode, result);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                } 
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Controller cập nhật món ăn
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>Bản ghi bị ảnh hưởng</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (24/04/2023)
        [HttpPut("{id}")]
        public IActionResult UpdateFood( [FromBody] FoodDetail foodDetail)
        {
            try
            {
                if (foodDetail != null)
                {
                    var result = _foodDetailBL.UpdateFood(foodDetail);
                    return StatusCode((int)result.StatusCode, result);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
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