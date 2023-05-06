using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Services
{
    public class ServiceHobbyBL : BaseBL<ServiceHobby> , IServiceHobbyBL
    {
        private IServiceHobbyDL _serviceHobbyDL;

        public ServiceHobbyBL(IServiceHobbyDL serviceHobbyDL) : base(serviceHobbyDL)
        {
            _serviceHobbyDL = serviceHobbyDL;
        }

        /// <summary>
        /// Lấy ra danh sách sở thích phục vụ thêm theo ID món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <returns>danh sách sở thích phục vụ thêm theo ID món ăn</returns>
        /// CreatedBy: NVTHUY (24/04/2023)
        public IEnumerable<ServiceHobby> GetListService(Guid foodID)
        {
            return _serviceHobbyDL.GetListService(foodID);
        }

    }
}
