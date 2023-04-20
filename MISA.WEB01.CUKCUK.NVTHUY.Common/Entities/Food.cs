using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class Food
    {
        public Guid FoodID { get; set; }

        public string FoodCode { get; set; }

        public string FoodName { get; set; }

        public Guid? MenuGroupID { get; set; }

        public string? MenuGroupName { get; set; } 

        public Guid FoodUnitID { get; set; }

        public string? FoodUnitName { get; set; }

        public Guid? CookRoomID { get; set; }
        
        public string? CookRoomName { get; set; }   

        public double FoodPrice { get; set; }

        public double? FoodCost { get; set; }

        public string? FoodDescription { get; set; }

        public string? FoodImageID { get; set; }

        public int? ShowOnMenu { get; set; }    

        public DateTime? ModifiedDate { get; set; }

        public List<ServiceHobby> ServiceHobbies { get; set; }

    }
}
