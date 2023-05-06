using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class MenuGroup : Base
    {
        /// <summary>
        /// ID nhóm thực đơn
        /// </summary>
        public Guid? MenuGroupID { get; set; }   

        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string? MenuGroupName { get; set; }
    }
}
