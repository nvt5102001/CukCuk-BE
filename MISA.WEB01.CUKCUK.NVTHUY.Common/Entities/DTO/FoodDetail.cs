using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO
{
    public class FoodDetail
    {
        /// <summary>
        /// Món ăn
        /// </summary>
        public Food food { get; set; }

        /// <summary>
        /// List sở thích phục vụ thêm
        /// </summary>
        public List<ServiceHobby>? serviceHobbies { get; set; }
    }
}
