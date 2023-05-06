using MISA.WEB01.CUKCUK.NVTHUY.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class Sort
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Sắp xếp theo thứ tự
        /// </summary>
        public Direction direction { get; set; }
    }
}
