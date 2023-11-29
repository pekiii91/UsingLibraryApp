using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public enum StatusEnum
    {
        Success = 1, Failed = 2, NoContent = 3, IsExist = 4
    }

    public enum StatusRequest
    {
        Approve = 12, Decline = 13, Pending = 14 
    }

}
