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
    public class FoodBL : BaseBL<Food> , IFoodBL
    {
        private IFoodDL _foodDL;

        public FoodBL(IFoodDL foodDL) : base(foodDL)
        {
            _foodDL = foodDL;
        }
    }
}
