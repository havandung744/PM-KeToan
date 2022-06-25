using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    public class ValidateException: Exception
    {
        IDictionary ErrorsMsg;
        public ValidateException(IDictionary Errors = null)
        {
            this.ErrorsMsg = Errors;
        }
        public override IDictionary Data => this.ErrorsMsg;

    }
}
