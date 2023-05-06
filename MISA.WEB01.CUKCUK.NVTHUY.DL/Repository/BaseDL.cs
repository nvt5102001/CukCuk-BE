using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.DL.Repository
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field

        protected string connectionString;
        protected MySqlConnection mySqlConnection;
        protected string className;
        protected List<string> errorList;

        #endregion

        public BaseDL(IConfiguration configuration)
        {
            errorList = new List<string>();
            // Khai báo thông tin kết nối
            connectionString = configuration.GetConnectionString("dataBase");
            className = typeof(T).Name;
            
        }

        public void OpenConnect()
        {
            mySqlConnection = new MySqlConnection(connectionString);
            if(mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
            {
                mySqlConnection.Open();
            }    
        }

       
        public void CloseConnect()
        {
            mySqlConnection.Close(); 
            mySqlConnection.Dispose();
        }

        public IEnumerable<T> GetALL()
        {
            OpenConnect();
            var procCommnand = $"Proc_Select_{className}_All";
            var result = mySqlConnection.Query<T>(procCommnand, commandType: System.Data.CommandType.StoredProcedure);
            if(result == null)
            {
                throw new ErrorException(devMsg: Resources.InvalidData);
            }
            CloseConnect();
            return result;
        }
    }
}
