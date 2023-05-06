using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class CookRoom: Base
    {   
        /// <summary>
        /// ID Nơi chế biến
        /// </summary>
        public Guid? CookRoomID { get; set; }   

        /// <summary>
        /// Tên nơi chế biến
        /// </summary>
        public string? CookRoomName { get; set; }
    }
}
