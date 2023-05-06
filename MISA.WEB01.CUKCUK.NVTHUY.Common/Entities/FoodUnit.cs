using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class FoodUnit : Base
    {
        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public Guid FoodUnitID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? FoodUnitName { get; set; }   


    }
}
