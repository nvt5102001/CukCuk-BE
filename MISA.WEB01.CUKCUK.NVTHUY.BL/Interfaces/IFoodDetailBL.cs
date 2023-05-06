using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces
{
    public interface IFoodDetailBL : IBaseBL<FoodDetail>
    {
        /// <summary>
        /// Thêm món ăn và sở thích phục vụ thêm
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>ID món ăn đã thêm</returns>
        /// CreatedBy: NVTHUY(25/04/2023)
        public ValidateResult InsertFoodDetail(FoodDetail foodDetail);

        /// <summary>
        /// Cập nhật món ăn
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>Bản ghi bị ảnh hưởng</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (24/04/2023)
        public ValidateResult UpdateFood(FoodDetail foodDetail);
    }
}
