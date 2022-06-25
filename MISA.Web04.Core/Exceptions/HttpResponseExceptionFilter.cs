using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is ValidateException)
            {
                var res = new
                {
                    devMsg = context.Exception.Message,
                    userMsg = "Dữ liệu đầu vào không hợp lệ",
                    Data = context.Exception.Data
                };
                context.Result = new ObjectResult(res)
                {
                    StatusCode = 400
                };

                // phải có dòng này thì mới ăn
                context.ExceptionHandled = true;


            }
        }

    }
}
