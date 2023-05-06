using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces
{
    public interface IFoodDL : IBaseDL<Food>
    {
        /// <summary>
        /// Lấy món ăn theo ID
        /// </summary>
        /// <param name="id">ID món ăn cần lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public Food GetByID(Guid id);

        /// <summary>
        /// Xoá 1 hoặc nhiều món ăn
        /// </summary>
        /// <param name="ids">Chuỗi chứa một hoặc nhiều mã món ăn</param>
        /// <returns>Số bản ghi bị xoá</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public int DeleteFood(string ids);

        /// <summary>
        /// Thêm món ăn
        /// </summary>
        /// <param name="food">Thông tin món ăn muốn thêmm </param>
        /// <returns>Mã món ăn mới thêm</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public Guid InsertFood(Food food);

        /// <summary>
        /// Cập nhật món ăn
        /// </summary>
        /// <param name="food">Món ăn cần cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NVTHUY(25/04/2023)
        public int UpdateFood(Food food);

        /// <summary>
        /// Gọi store lọc và phân trang
        /// </summary>
        /// <param name="m_where">Điều kiện where</param>
        /// <param name="m_sort">Điều kiện sort</param>
        /// <param name="m_paging">limit và offset</param>
        /// <returns>Danh sách lọc và tổng số bản ghi đã lọc</returns>
        /// CreatedBy: NVTHUY(24/04/2023)
        public dynamic FilterFood(string m_where, string? m_sort, string m_paging);

        /// <summary>
        /// Kiểm tra trùng mã món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <param name="foodCode">Mã món ăn</param>
        /// <returns>Trả về true nếu trùng mã, false nếu không trùng mã</returns>
        /// CreatedBy: NVTHUY(24/04/2023)
        public bool IsDuplicate(Guid? foodID, string foodCode);

        public string GetNewCodeNumber(string foodCode);

       
    }
}
