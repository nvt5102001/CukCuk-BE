using MISA.WEB01.CUKCUK.NVTHUY.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Entities
{
    public class Food : Base
    {
        /// <summary>
        /// ID món ăn
        /// </summary>
        public Guid FoodID { get; set; }

        /// <summary>
        /// Mã món ăn
        /// </summary>
        [NotAllowedNull, NotAllowedDuplicate, PropsName("Mã món ăn")]
        public string FoodCode { get; set; }

        /// <summary>
        /// Tên món ăn
        /// </summary>
        [NotAllowedNull, PropsName("Tên món ăn")]
        public string FoodName { get; set; }

        /// <summary>
        /// ID nhóm thực đơn
        /// </summary>
        public Guid? MenuGroupID { get; set; }

        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string? MenuGroupName { get; set; }

        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        [NotAllowedNull, PropsName("Đơn vị tính")]
        public Guid FoodUnitID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? FoodUnitName { get; set; }

        /// <summary>
        /// ID nơi chế biến
        /// </summary>
        public Guid? CookRoomID { get; set; }
        
        /// <summary>
        /// Tên nơi chế biến
        /// </summary>
        public string? CookRoomName { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        [NotAllowedNull, PropsName("Giá bán")]
        public double FoodPrice { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        public double? FoodCost { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? FoodDescription { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        public string? FoodImageID { get; set; }

        /// <summary>
        /// Hiện ở menu
        /// </summary>
        public int? ShowOnMenu { get; set; }

        /// <summary>
        /// Ngừng bán
        /// </summary>
        public int? StopSelling { get; set; }


    }
}
