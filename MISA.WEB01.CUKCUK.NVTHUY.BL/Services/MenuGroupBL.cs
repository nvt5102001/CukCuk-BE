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
    public class MenuGroupBL : BaseBL<MenuGroup> , IMenuGroupBL
    {
        private IMenuGroupDL _menuGroupDL;

        public MenuGroupBL(IMenuGroupDL menuGroupDL) : base(menuGroupDL)
        {
            _menuGroupDL = menuGroupDL;
        }
    }
}
