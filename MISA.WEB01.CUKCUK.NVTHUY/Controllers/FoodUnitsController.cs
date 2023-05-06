using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;

namespace MISA.WEB01.CUKCUK.NVTHUY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodUnitsController : BaseController<FoodUnit>
    {
        #region Properties

        private IFoodUnitBL _foodUnitBL;

        #endregion

        #region Contructor

        public FoodUnitsController(IFoodUnitBL foodUnitBL) : base(foodUnitBL)
        {
            _foodUnitBL = foodUnitBL;
        }

        #endregion
    }
}
