using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuGroupsController : BaseController<MenuGroup>
    {
        private IMenuGroupBL _menuGroupBL;

        public MenuGroupsController(IMenuGroupBL menuGroupBL) : base(menuGroupBL)
        {
            _menuGroupBL = menuGroupBL;
            
        }
    }
}
