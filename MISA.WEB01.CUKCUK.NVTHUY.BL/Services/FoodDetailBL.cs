using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Attributes;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Repository;
using MySql.Data.MySqlClient;
using MySqlConnector;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using MySqlTransaction = MySql.Data.MySqlClient.MySqlTransaction;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Services
{
    public class FoodDetailBL : BaseBL<FoodDetail>, IFoodDetailBL
    {
        #region Properties

        private IFoodDetailDL _foodDetailDL;
        private IFoodDL _foodDL;
        private IServiceHobbyDL _serviceHobbyDL;
        private IConfiguration _configuration;
        protected List<string> ErrorValidateMsgs;

        #endregion

        #region Properties

        public FoodDetailBL(IFoodDetailDL foodDetailDL, IFoodDL foodDL, IServiceHobbyDL serviceHobbyDL, IConfiguration configuration) : base(foodDetailDL)
        {
            _foodDetailDL = foodDetailDL;
            _foodDL = foodDL;
            _serviceHobbyDL = serviceHobbyDL;
            _configuration = configuration;
        }

        #endregion


        #region Properties

        public ValidateResult ValidateObject(object obj, Guid? id)
        {
            var errorList = new List<string>();
            Type objectType = obj.GetType();
            foreach (PropertyInfo property in objectType.GetProperties())
            {
                bool isNotAllowedNull = property.IsDefined(typeof(NotAllowedNull), true);
                bool isNotAllowedDuplicate = property.IsDefined(typeof(NotAllowedDuplicate), true);
                string propName = property.IsDefined(typeof(PropsName), true)
                    ? ((PropsName)property.GetCustomAttribute(typeof(PropsName), true)).Name
                    : property.Name;

                // Kiểm tra thuộc tính NotAllowedNull
                if (isNotAllowedNull)
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        Guid propValue = (Guid)property.GetValue(obj);
                        if (Guid.Equals(propValue, Guid.Empty))
                        {
                            errorList.Add(String.Format(Validate.NotAllowedNull, propName));
                        }
                    }
                    else if (string.IsNullOrEmpty(property.GetValue(obj)?.ToString()))
                    {
                        errorList.Add(String.Format(Validate.NotAllowedNull, propName));
                    }
                }

                // Kiểm tra thuộc tính NotAllowedDuplicate (chỉ xét kiểu string)
                if (isNotAllowedDuplicate && property.PropertyType == typeof(string))
                {
                    string? propValue = property.GetValue(obj)?.ToString();
                    if (!string.IsNullOrEmpty(propValue) && _foodDL.IsDuplicate(id, propValue))
                    {
                        errorList.Add(String.Format(Validate.NotAllowedDuplicate, propName));
                    }
                }
            }
            if (errorList.Count > 0)
            {
                return new ValidateResult
                {
                    Timestamp = DateTime.Now,
                    ListErrors = errorList
                };
            }
            else
            {
                return new ValidateResult
                {
                    ListErrors = null
                };
            }
        }

        /// <summary>
        /// Thêm món ăn và sở thích phục vụ thêm
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>ID món ăn đã thêm</returns>
        /// CreatedBy: NVTHUY(25/04/2023)
        public ValidateResult InsertFoodDetail(FoodDetail foodDetail)
        {
            if (foodDetail.food != null)
            {
                var IsValid = ValidateObject(foodDetail.food, foodDetail.food.FoodID);
                if (IsValid.ListErrors == null)
                {
                    
                    var res = _foodDL.InsertFood(foodDetail.food);
                    if (!Guid.Equals(res, Guid.Empty))
                    {
                        if (foodDetail.serviceHobbies != null)
                        {
                            if (foodDetail.serviceHobbies.Count > 0)
                            {
                                for (var i = 0; i < foodDetail.serviceHobbies.Count; i++)
                                {
                                           
                                    if (foodDetail.serviceHobbies[i].ServiceHobbyName == null || string.IsNullOrWhiteSpace(foodDetail.serviceHobbies[i].ServiceHobbyName))
                                    {
                                        continue;
                                    }
                                    foodDetail.serviceHobbies[i].FoodID = res;
                                    _serviceHobbyDL.InsertServiceHobby(foodDetail.serviceHobbies[i]);
                                }
                            }
                        }
                                
                        return new ValidateResult()
                        {
                            StatusCode = 201,
                            ListErrors = new List<string>() { "1" }
                        };
                    }
                    else
                    {
                        return new ValidateResult()
                        {
                            StatusCode = 400,
                            ListErrors = new List<string>() { "0" }
                        };
                    }
                }
                else
                {
                    return IsValid;
                }
            }
            else
            {
                throw new ErrorException(devMsg: Resources.ExceptionInsert);
            }
        }


        /// <summary>
        /// Cập nhật món ăn
        /// </summary>
        /// <param name="foodDetail">Món ăn và list sở thích phụ vụ thêm</param>
        /// <returns>Bản ghi bị ảnh hưởng</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (24/04/2023)
        public ValidateResult UpdateFood(FoodDetail foodDetail)
        {
            // Kiểm tra foodDetail.food không null
            if (foodDetail.food != null)
            {
                var IsValid = ValidateObject(foodDetail.food, foodDetail.food.FoodID);
                if (IsValid.ListErrors == null)
                {
                    var res = _foodDL.UpdateFood(foodDetail.food); // Cập nhật thông tin món ăn
                    if (res > 0)
                    {
                        // Xóa các dịch vụ liên quan đến món ăn
                        var deleteService = _serviceHobbyDL.DeleteServiceHobby(foodDetail.food.FoodID);
                        if (deleteService >= 0)
                        {
                            if (foodDetail.serviceHobbies != null)
                            {
                                if (foodDetail.serviceHobbies.Count > 0)
                                {
                                    for (var i = 0; i < foodDetail.serviceHobbies.Count; i++)
                                    {
                                        if (foodDetail.serviceHobbies[i].ServiceHobbyName == null || string.IsNullOrWhiteSpace(foodDetail.serviceHobbies[i].ServiceHobbyName))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            foodDetail.serviceHobbies[i].FoodID = foodDetail.food.FoodID;
                                            _serviceHobbyDL.InsertServiceHobby(foodDetail.serviceHobbies[i]);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new ErrorException(devMsg: Resources.ExceptionInsert);
                        }

                        return new ValidateResult()
                        {
                            StatusCode = 200,
                            ListErrors = new List<string>() { "1" }
                        };
                    }
                    else
                    {
                        return new ValidateResult()
                        {
                            StatusCode = 400,
                            ListErrors = new List<string>() { "0" }
                        };
                    }
                }
                else
                {
                    return IsValid;
                }
            }
            else
            {
                throw new ErrorException(devMsg: Resources.ExceptionInsert);
            }
        }


        #endregion
    }
}
