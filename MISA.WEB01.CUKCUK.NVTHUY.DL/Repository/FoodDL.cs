using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Repository
{
    public class FoodDL : BaseDL<Food>, IFoodDL
    {
        #region Contructor

        public FoodDL(IConfiguration configuration) : base(configuration)
        {

        }


        #endregion

        #region Methods




        /// <summary>
        /// Lấy món ăn theo ID
        /// </summary>
        /// <param name="id">ID món ăn cần lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// <exception cref="ErrorException"></exception>
        /// CreatedBy: NVTHUY (22/04/2023)
        public Food GetByID(Guid id)
        {
            OpenConnect();
            var procCommand = $"Proc_Select_Food_ByID";
            var paramProc = new DynamicParameters();
            paramProc.Add($"m_FoodID", id);

            var result = mySqlConnection.QueryFirstOrDefault<Food>(procCommand, param: paramProc, commandType: System.Data.CommandType.StoredProcedure);
            if (result == null)
            {
                throw new ErrorException(devMsg: Resources.NullData);
            }
            CloseConnect();
            return result;
        }


        /// <summary>
        /// Xoá 1 hoặc nhiều món ăn
        /// </summary>
        /// <param name="ids">Chuỗi chứa một hoặc nhiều mã món ăn</param>
        /// <returns>Số bản ghi bị xoá</returns>
        /// CreatedBy: NVTHUY (22/04/2023)
        public int DeleteFood(string ids)
        {
            OpenConnect();
            using (var transaction = mySqlConnection.BeginTransaction())
            {
                try
                {
                    var procCommand = $"Proc_Delete_Food";
                    var paramProc = new DynamicParameters();
                    paramProc.Add($"m_FoodIDs", ids);
                    string[] listFoodID = ids.Split(',');
                    int count = listFoodID.Length;
                    var result = mySqlConnection.Execute(procCommand, param: paramProc, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (result == count)
                    {
                        transaction.Commit(); // thực hiện commit transaction
                        return result;
                    }
                    else
                    {
                        transaction.Rollback(); // thực hiện rollback transaction
                        throw new ErrorException(devMsg: Resources.ExceptionDelete);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback(); // thực hiện rollback transaction
                    throw new ErrorException(devMsg: Resources.ExceptionDelete);
                }
                finally
                {
                    CloseConnect();
                }
            }
        }

        /// <summary>
        /// Gọi store lọc và phân trang
        /// </summary>
        /// <param name="m_where">Điều kiện where</param>
        /// <param name="m_sort">Điều kiện sort</param>
        /// <param name="m_paging">limit và offset</param>
        /// <returns>Danh sách lọc và tổng số bản ghi đã lọc</returns>
        /// CreatedBy: NVTHUY(24/04/2023)
        public dynamic FilterFood(string m_where, string? m_sort, string m_paging)
        {
            OpenConnect();
            var procCommand = $"Proc_Select_Food_Paging";
            var paramProc = new DynamicParameters();
            paramProc.Add($"m_where", m_where);
            paramProc.Add($"m_paging", m_paging);
            paramProc.Add($"m_sort", m_sort);

            var result = mySqlConnection.QueryMultiple(procCommand, param: paramProc, commandType: System.Data.CommandType.StoredProcedure);
            var listFood = result.Read<Food>().ToList();
            var totalRecord = result.Read<int>().Single();
            return new
            {
                TotalRecord = totalRecord,
                Data = listFood
            };

        }

        /// <summary>
        /// Gọi store thêm món ăn
        /// </summary>
        /// <param name="food">Món ăn cần thêm</param>
        /// <returns>Mã món ăn mới được thêm</returns>
        /// CreatedBy: NVTHUY(25/04/2023)
        public Guid InsertFood(Food food)
        {
            OpenConnect();
            using (var transaction = mySqlConnection.BeginTransaction())
            {
                try
                {
                    var procCommnand = $"Proc_Insert_Food";
                    var paramProc = new DynamicParameters(food);
                    var newGuid = Guid.NewGuid();
                    food.FoodID = newGuid;
                    var result = mySqlConnection.Execute(procCommnand, param: paramProc, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    if (result == 0)
                    {
                        throw new ErrorException(devMsg: Resources.ExceptionInsert);
                    }
                    else
                    {
                        transaction.Commit();
                        return newGuid;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;

                }
                finally
                {
                    CloseConnect();
                }
            }
        } 

        /// <summary>
        /// Gọi store cập nhật món ăn
        /// </summary>
        /// <param name="food">Món ăn cần cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NVTHUY(25/04/2023)
        public int UpdateFood(Food food)
        {
            OpenConnect();
            using (var transaction = mySqlConnection.BeginTransaction())
            {
                try
                {
                    var procCommnand = "Proc_Update_Food";
                    var paramProc = new DynamicParameters(food);
                    var result = mySqlConnection.ExecuteScalar<int>(procCommnand, param: paramProc, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    
                    if (result > 0)
                    {
                        transaction.Commit(); // thực hiện commit transaction
                        return result;
                    }
                    else
                    {
                        transaction.Rollback(); // thực hiện rollback transaction
                        throw new ErrorException(devMsg: Resources.ExceptionUpdate);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;

                }
                finally
                {
                    CloseConnect();
                }
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <param name="foodCode">Mã món ăn</param>
        /// <returns>Trả về true nếu trùng mã, false nếu không trùng mã</returns>
        /// CreatedBy: NVTHUY(24/04/2023)
        public bool IsDuplicate(Guid? foodID, string foodCode)
        {
            OpenConnect();
            try
            {
                var procCommand = $"Proc_Food_DuplicateCode";
                var paramProc = new DynamicParameters();
                paramProc.Add($"m_FoodID", foodID);
                paramProc.Add($"m_FoodCode", foodCode);

                var result = mySqlConnection.QueryFirstOrDefault(procCommand, param: paramProc, commandType: System.Data.CommandType.StoredProcedure);

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                CloseConnect();
            }
        }

        /// <summary>
        /// Lấy mã lớn nhất + 1
        /// </summary>
        /// <param name="foodCode">Mã món ăn</param>
        /// <returns>Trả về mã mới</returns>
        /// CreatedBy: NVTHUY(24/04/2023)
        public string GetNewCodeNumber(string foodCode)
        {
            OpenConnect();
            try
            {
                var procCommand = $"Proc_Select_NewCodeNumber";
                var paramProc = new DynamicParameters();
                paramProc.Add($"m_FoodCode", foodCode);

                var result = mySqlConnection.ExecuteScalar<string>(procCommand, param: paramProc, commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    throw new ErrorException(devMsg: Resources.NullData);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                CloseConnect();
            }
        }

        #endregion

    }
}
