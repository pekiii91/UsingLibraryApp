using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class Response<T> : IResponse<T>
    {
        public T Value { get; set; }
        public string Message { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class NoValue
    {

    }
}
