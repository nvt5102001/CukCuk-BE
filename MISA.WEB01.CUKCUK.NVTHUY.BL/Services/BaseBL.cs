using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Attributes;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities.DTO;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Resources;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.BL.Services
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Properties

        private IBaseDL<T> _baseDL;
        protected List<string> errorList;
        protected bool IsValid = true;

        #endregion

        #region Contructor

        public BaseBL(IBaseDL<T> baseDL) 
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy NVThuy 20/04/2023
        public IEnumerable<T> GetAll()
        {
            return _baseDL.GetALL();
        }

        #endregion
    }
}
