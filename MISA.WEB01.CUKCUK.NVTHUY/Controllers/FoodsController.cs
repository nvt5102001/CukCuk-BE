using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using System.Xml.Linq;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodsController : BaseController<Food>
    {
        #region Properties

        private IFoodBL _foodBL;

        #endregion

        #region Contructor

        public FoodsController(IFoodBL foodBL) : base(foodBL)
        {
            _foodBL = foodBL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Controller xoá 1 hoặc nhiều món ăn
        /// </summary>
        /// <param name="ids">Chuỗi chứa một hoặc nhiều mã món ăn</param>
        /// <returns>Số bản ghi bị xoá</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        [HttpDelete]
        public IActionResult DeleteFood([FromBody] string ids)
        {
            try
            {
                if (ids != null && ids.Length > 0)
                {
                    // Gọi service lọc và phân trang danh sách thực đơn
                    var res = _foodBL.DeleteFood(ids);

                    // Trả kết quả về cho client
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        /// <summary>
        /// Controller upload ảnh lên server
        /// </summary>
        /// <param name="fileImage">Obj ảnh</param>
        /// <returns>Tên của ảnh trên server</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        [HttpPost("upload-image")]
        public IActionResult UploadImage(IFormFile fileImage)
        {
            try
            {
                if(fileImage != null)
                {
                    // Gọi service upload ảnh
                    var res = _foodBL.UploadImage(fileImage);

                    // Trả lại kết quả cho client
                    return Ok(res);
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

        /// <summary>
        /// Xoá ảnh trong thư mục
        /// </summary>
        /// <param name="fileName">Tên ảnh muốn xoá</param>
        /// <returns>Xoá ảnh thành công</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY(28/04/2023)
        [HttpPost("remove-image")]
        public IActionResult DeleteImage(string fileName)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(fileName))
                {
                    _foodBL.DeleteImage(fileName);
                    return Ok();
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                } 
                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Controller hiển thị ảnh
        /// </summary>
        /// <param name="imageName">tên ảnh trên server</param>
        /// <returns>Hiển thị ảnh</returns>
        /// CreatedBy: NVTHUY (26/04/2023)
        [HttpGet("get-image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(imageName))
                {
                    var imageResult = _foodBL.GetImage(imageName);

                    if (imageResult == null)
                    {
                        return NotFound();
                    }

                    return File(imageResult.ImageBytes, imageResult.MimeType);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy món ăn theo ID
        /// </summary>
        /// <param name="id">ID món ăn cần lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                if (!Guid.Equals(id, Guid.Empty))
                {
                    // Gọi service lọc và phân trang danh sách thực đơn
                    var res = _foodBL.GetByID(id);

                    // Trả kết quả về cho client
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        /// <summary>
        /// Controller lọc và sắp xếp bản ghi
        /// </summary>
        /// <param name="filter">Điều kiện lọc và sort dữ liệu </param>
        /// <returns>Danh sách bản ghi theo điều kiện lọc và sắp xếp</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        [HttpPost("filter")]
        public IActionResult FilterFood(Filter filter)
        {
            try
            {
                if (filter != null)
                {
                    // Gọi service lọc và phân trang danh sách thực đơn
                    var res = _foodBL.FilterFood(filter);

                    // Trả kết quả về cho client
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }


            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        /// <summary>
        /// Controller lấy ra mã mới
        /// </summary>
        /// <param name="name">Tên món ăn để lấy mã mới</param>
        /// <returns>Mã mới</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        [HttpGet("new-code")]
        public IActionResult GetNewCode(String name)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var res = _foodBL.GetNewCode(name);

                    // Trả kết quả về cho client
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <param name="foodCode">Mã món ăn</param>
        /// <returns>true- nếu trùng mã, false- nếu không trùng mã</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY(28/04/2023)
        [HttpGet("duplicate-code")]
        public IActionResult IsDuplicate(Guid? foodID, string foodCode)
        {
            try
            {
                if (foodID != null && !string.IsNullOrEmpty(foodCode))
                {
                    var res = _foodBL.IsDuplicate(foodID, foodCode);

                    // Trả kết quả về cho client
                    return Ok(res);
                }
                else
                {
                    throw new ErrorException(devMsg: Resources.InputNullData);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

    }
    #endregion

}
