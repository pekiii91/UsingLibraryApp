using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IResponse<T>
    {
        T Value { get; set; }
        StatusEnum Status { get; set; }
        string Message { get; set; }
    }
}
