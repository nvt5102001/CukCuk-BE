using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CookRoomsController : BaseController<CookRoom>
    {
        private ICookRoomBL _cookRoomBL;

        public CookRoomsController(ICookRoomBL cookRoomBL) : base(cookRoomBL)
        {
            _cookRoomBL = cookRoomBL;
        }
    }
}
