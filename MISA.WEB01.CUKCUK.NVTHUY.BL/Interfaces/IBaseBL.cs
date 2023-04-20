using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces
{
    public interface IBaseBL<T>
    {
        /// <summary>
        ///Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy NVThuy 20/04/2023
        /// 
        public IEnumerable<T> GetAll();
    }
}
