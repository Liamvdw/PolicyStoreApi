using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApi.Model
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
