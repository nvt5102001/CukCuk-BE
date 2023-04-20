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
    public class CookRoomBL : BaseBL<CookRoom> , ICookRoomBL
    {
        private ICookRoomDL _cookRoomDL;
        public CookRoomBL(ICookRoomDL cookRoomDL) : base(cookRoomDL)
        {
            _cookRoomDL = cookRoomDL;
        }
    }
}
