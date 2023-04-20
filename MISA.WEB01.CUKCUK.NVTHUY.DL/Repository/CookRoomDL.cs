using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Repository
{
    public class CookRoomDL : BaseDL<CookRoom> , ICookRoomDL
    {
        public CookRoomDL(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
