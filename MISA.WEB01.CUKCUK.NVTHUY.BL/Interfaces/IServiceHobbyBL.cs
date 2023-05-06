using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces
{
    public interface IServiceHobbyBL : IBaseBL<ServiceHobby>
    {
        /// <summary>
        /// Lấy ra danh sách sở thích phục vụ thêm theo ID món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <returns>danh sách sở thích phục vụ thêm theo ID món ăn</returns>
        /// CreatedBy: NVTHUY (24/04/2023)
        public IEnumerable<ServiceHobby> GetListService(Guid foodID);
    }
}
