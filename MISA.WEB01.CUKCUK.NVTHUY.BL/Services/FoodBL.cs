using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Enums;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Services
{
    public class FoodBL : BaseBL<Food> , IFoodBL
    {
        #region Properties

        private IFoodDL _foodDL;

        #endregion

        #region Contructor

        public FoodBL(IFoodDL foodDL, IConfiguration configuration ) : base(foodDL)
        {
            _foodDL = foodDL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Upload ảnh lên server
        /// </summary>
        /// <param name="fileImage">Obj ảnh</param>
        /// <returns>Tên của ảnh trên server</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public string UploadImage(IFormFile fileImage)
        {
            var supportTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExt = Path.GetExtension(fileImage.FileName);

            // 1. Validate tệp
            // Check định dạng
            if (!supportTypes.Contains(fileExt))
            {
                throw new ErrorException(devMsg: "Tệp không đúng định dạng");
            }
            // Check dung lượng file ảnh
            if (fileImage.Length > 5 * 1024 * 1024)
            {
                throw new ErrorException(devMsg: "Ảnh không tải được do vượt quá dung lượng. Vui lòng chọn ảnh có dung lượng nhỏ hơn 5 MB");
            }

            string fileName = Guid.NewGuid() + fileExt;

            // Tạo đường dẫn
            var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "UploadFile", "Images", "Food");

            if (!Directory.Exists(pathBuild))
            {
                Directory.CreateDirectory(pathBuild);
            }

            // Khởi tạo đường dẫn ảnh
            var path = Path.Combine(pathBuild, fileName);

            // Copy ảnh theo đường dẫn
            using (var stream = new FileStream(path, FileMode.Create))
            {
                fileImage.CopyTo(stream);
            }
            return fileName;
        }

        /// <summary>
        /// Xoá ảnh trong thư mục
        /// </summary>
        /// <param name="fileName">Tên ảnh muốn xoá</param>
        /// <returns>Xoá ảnh thành công</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY(28/04/2023)
        public void DeleteImage(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFile", "Images", "Food", imageName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        /// <summary>
        /// Lấy món ăn theo ID
        /// </summary>
        /// <param name="id">ID món ăn cần lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        public Food GetByID(Guid id)
        {
            return _foodDL.GetByID(id);
        }

        /// <summary>
        /// Xoá 1 hoặc nhiều món ăn
        /// </summary>
        /// <param name="ids">Chuỗi chứa một hoặc nhiều mã món ăn</param>
        /// <returns>Số bản ghi bị xoá</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public int DeleteFood(string ids)
        {
            return _foodDL.DeleteFood(ids);
        }

        /// <summary>
        /// Lấy ra mã mới
        /// </summary>
        /// <param name="name">Tên món ăn để lấy mã mới</param>
        /// <returns>Mã mới</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        public string GetNewCode(string name)
        {
            var words = name.Trim().Split(' ');

            string foodCode = "";

            if (words.Length < 2)
            {
                foodCode = convertToUnSign(words[0]).ToUpper();
                if (_foodDL.IsDuplicate(Guid.Empty, foodCode) == false)
                {
                    return foodCode;
                }
                else
                {
                    return _foodDL.GetNewCodeNumber(foodCode);
                }
            }
            else
            {
                foreach (var word in words)
                {
                    foodCode += word[0];
                }
                foodCode = convertToUnSign(foodCode).ToUpper();

                if (_foodDL.IsDuplicate(Guid.Empty, foodCode) == false)
                {
                    return foodCode;
                }
                else
                {
                    foodCode = "";
                    foreach (var word in words)
                    {
                        foodCode += word;
                    }
                    foodCode = convertToUnSign(foodCode).ToUpper();

                    if (_foodDL.IsDuplicate(Guid.Empty, foodCode) == false)
                    {
                        return foodCode;
                    }
                    else
                    {
                        foodCode = "";
                        foreach (var word in words)
                        {
                            foodCode += word[0];
                        }
                        foodCode = convertToUnSign(foodCode).ToUpper();
                        return _foodDL.GetNewCodeNumber(foodCode);
                    }
                }
            }     
        }

        /// <summary>
        /// Loại bỏ dấu của chuỗi
        /// </summary>
        /// <param name="s">Chuỗi</param>
        /// <returns>chuỗi không chứa dấu</returns>
        private string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

         /// <summary>
        /// Controller lọc và sắp xếp bản ghi
        /// </summary>
        /// <param name="filter">Điều kiện lọc và sort dữ liệu </param>
        /// <returns>Danh sách bản ghi theo điều kiện lọc và sắp xếp</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        public object FilterFood(Filter filter)
        {
            string m_Paging;
            string m_Where = "WHERE 1 = 1 ";
            string? m_Sort = null;

            if (filter.Filters.Count > 0)
            {
                m_Where += BuildWhereCommandText(filter.Filters);
            }

            if (filter.Sort != null)
            {
                m_Sort = BuildSortCommandText(filter.Sort);
            }

            var offset = (filter.Page - 1) * filter.Limit;

            m_Paging = $"Limit {filter.Limit} OFFSET {offset}";

            var res = _foodDL.FilterFood(m_Where, m_Sort, m_Paging);

            int totalRecord = GetValueObject(res, "TotalRecord");

            var data = GetValueObject(res, "Data");

            var totalPage = Math.Floor(Convert.ToDecimal((totalRecord + filter.Limit - 1) / filter.Limit));

            return new
            {
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Data = data,
            };
        }

        /// <summary>
        /// Tạo câu lệnh where để lọc 
        /// </summary>
        /// <param name="filters">List các điều kiện lọc</param>
        /// <returns>Câu lệnh where</returns>
        /// CreatedBy: NVTHUY(26/04/2023)
        public string BuildWhereCommandText(List<FilterItem> filters)
        {
            string result = String.Empty;

            foreach (FilterItem filterItem in filters)
            {
                if (filterItem.Value != null && !String.IsNullOrWhiteSpace(filterItem.Value.ToString()))
                {
                    if (!filterItem.Value.ToString().Contains("%") && !filterItem.Value.ToString().Contains("'") && !filterItem.Value.ToString().Contains("_"))
                    {
                        switch (filterItem.Operator)
                        {
                            case Operator.Contain:
                                result += $"and {filterItem.Property} LIKE CONCAT('%','{filterItem.Value}','%') ";
                                break;
                            case Operator.Equal:
                                if (filterItem.Type == "string")
                                    result += $"and {filterItem.Property} = '{filterItem.Value}' ";
                                else if (filterItem.Type == "boolean" && Convert.ToInt16(filterItem.Value.ToString()) == 0)
                                    result += $"and ({filterItem.Property} = {filterItem.Value} OR {filterItem.Property} IS NULL) ";
                                else
                                    result += $"and {filterItem.Property} = {filterItem.Value} ";
                                break;
                            case Operator.Start_With:
                                result += $"and {filterItem.Property} LIKE CONCAT('{filterItem.Value}','%') ";
                                break;
                            case Operator.End_With:
                                result += $"and {filterItem.Property} LIKE CONCAT('%','{filterItem.Value}') ";
                                break;
                            case Operator.Not_Contain:
                                result += $"and {filterItem.Property} NOT LIKE CONCAT('%','{filterItem.Value}','%') ";
                                break;
                            case Operator.Less:
                                result += $"and {filterItem.Property} < {filterItem.Value} ";
                                break;
                            case Operator.Less_Or_Equal:
                                result += $"and {filterItem.Property} <= {filterItem.Value} ";
                                break;
                            case Operator.Bigger:
                                result += $"and {filterItem.Property} > {filterItem.Value} ";
                                break;
                            case Operator.Bigger_Or_Equal:
                                result += $"and {filterItem.Property} >= {filterItem.Value} ";
                                break;
                        }
                    }
                    else
                    {
                        result += $"and 1 = 0";
                    }  
                }
            }
            return result;
        }

        /// <summary>
        /// Tạo câu lệnh sort để sắp xếp
        /// </summary>
        /// <param name="filters">điều kiện sắp xếp</param>
        /// <returns>Câu lệnh sort</returns>
        /// CreatedBy: NVTHUY(26/04/2023)
        public string BuildSortCommandText(Sort sort)
        {
            string result = string.Empty;

            if (sort.direction == Direction.ASC)
                result = $"Order by {sort.Property} ASC";
            else if (sort.direction == Direction.DESC)
                result = $"Order by {sort.Property} DESC";
            return result;
        }

        /// <summary>
        /// Lấy giá trị của đối tượng
        /// </summary>
        /// <param name="obj">Đối tượng</param>
        /// <param name="propertyName">Tên thuộc tính</param>
        /// <returns></returns>
        private object GetValueObject(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(obj);
            }
            return null;
        }

        /// <summary>
        /// Hiển thị ảnh
        /// </summary>
        /// <param name="imageName">tên ảnh trên server</param>
        /// <returns>Hiển thị ảnh</returns>
        /// CreatedBy: NVTHUY (26/04/2023)
        public ImageResult GetImage(string imageName)
        {
            string fileExtension = Path.GetExtension(imageName);

            if (string.IsNullOrEmpty(fileExtension) ||
                (fileExtension.ToLower() != ".png" &&
                 fileExtension.ToLower() != ".jpg" &&
                 fileExtension.ToLower() != ".jpeg" &&
                 fileExtension.ToLower() != ".gif"))
            {
                return null;
            }

            string imagePath = Path.Combine("UploadFile/Images/Food", imageName);
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            string mimeType;
            switch (fileExtension.ToLower())
            {
                case ".png":
                    mimeType = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                default:
                    mimeType = "application/octet-stream";
                    break;
            }

            return new ImageResult { ImageBytes = imageBytes, MimeType = mimeType };
        }

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <param name="foodCode">Mã món ăn</param>
        /// <returns>true- nếu trùng mã, false- nếu không trùng mã</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY(28/04/2023)
        public bool IsDuplicate(Guid? foodID, string foodCode)
        {
            return _foodDL.IsDuplicate(foodID, foodCode);
        }

        #endregion
    }
}
