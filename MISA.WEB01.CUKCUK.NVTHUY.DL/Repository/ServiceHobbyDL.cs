using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Repository
{
    public class ServiceHobbyDL : BaseDL<ServiceHobby> , IServiceHobbyDL
    {
        public ServiceHobbyDL (IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Thêm sở thích phục vụ thêm
        /// </summary>
        /// <param name="serviceHobby">Sở thích phục vụ thêm</param>
        /// <returns>Số sở thích được thêm</returns>
        /// CreatedBy: NVTHUY(27/04/2023)
        public int InsertServiceHobby(ServiceHobby serviceHobby)
        {
            OpenConnect();
            using (var transaction = mySqlConnection.BeginTransaction())
            {
                try
                {
                    var procCommnand = $"Proc_Insert_ServiceHobby";
                    var paramProc = new DynamicParameters(serviceHobby);
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
        /// Xoá các sở thích phục vụ thêm từ ID món ăn
        /// </summary>
        /// <param name="foodID">ID món ăn</param>
        /// <returns>Số sở thích bị xoá </returns>
        /// CreatedBy: NVTHUY(27/04/2023)
        public int DeleteServiceHobby(Guid foodID)
        {
            OpenConnect();
            using (var transaction = mySqlConnection.BeginTransaction())
            {
                try
                {
                    var procCommnand = $"Proc_Delete_ServiceHobby_ByFoodID";
                    var paramProc = new DynamicParameters();
                    paramProc.Add($"m_FoodID", foodID);
                    var result = mySqlConnection.ExecuteScalar<int>(procCommnand, param: paramProc, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    if (result >= 0)
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
        /// Lấy ra các sở thích phục vụ thêm từ món ăn
        /// </summary>
        /// <param name="foodID">Mã món ăn </param>
        /// <returns>Danh sách sở thích phục vụ thêm từ mã ID truyền vào</returns>
        /// CreatedBy: NVTHUY(27/04/2023)
        public IEnumerable<ServiceHobby> GetListService(Guid foodID)
        {
            OpenConnect();
            try
            {
                var procCommnand = $"Proc_Select_ServiceHobby_ByFoodID";
                var paramProc = new DynamicParameters();
                paramProc.Add($"m_FoodID", foodID);
                var result = mySqlConnection.Query<ServiceHobby>(procCommnand, param: paramProc, commandType: System.Data.CommandType.StoredProcedure);
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

    }
}
