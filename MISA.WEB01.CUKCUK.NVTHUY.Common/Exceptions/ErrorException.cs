using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Exceptions
{
    public class ErrorException : Exception
    {
        public string devMsg;

        public IDictionary errors;

        public ErrorException(string devMsg = null , List<string> errorLists = null )
        {
            this.devMsg = devMsg;
            errors = new Dictionary<string, List<string>>();
            errors.Add("Error", errorLists);
        }

        public override string Message => this.devMsg;

        public override IDictionary Data => this.errors;
    }
}
