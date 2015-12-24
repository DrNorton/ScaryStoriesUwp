using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaryStoriesUniversal.Api.CustomApi
{
    public class ApiResult<T>
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public T Result { get; set; }
    }
}
