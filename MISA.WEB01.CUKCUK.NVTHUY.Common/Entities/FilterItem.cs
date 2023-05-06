using MISA.WEB01.CUKCUK.NVTHUY.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class FilterItem
    {
        /// <summary>
        /// Thuộc tính lọc
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Kiểu filer
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// Giá trị lọc
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        public string Type { get; set; }
    }
}
