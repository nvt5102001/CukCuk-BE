using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class Filter
    {
        /// <summary>
        /// Danh sách các cột được lọc
        /// </summary>
        public List<FilterItem>? Filters { get; set; }

        /// <summary>
        /// Tổng số bản ghi trên 1 trang
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Sắp xếp theo cột
        /// </summary>
        public Sort? Sort { get; set; }
    }
}
