using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces
{
    public interface IBaseDL<T>
    {
        /// <summary>
        ///Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy NVThuy 20/04/2023
        /// 
        public IEnumerable<T> GetALL();
    }
}
