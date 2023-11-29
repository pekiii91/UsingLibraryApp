using Library.Models.UsersHelperModels;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.InterfaceService
{
    public interface IAdminService
    {
        Task<IResponse<List<UserModel>>> GetUsers();
        Task<IResponse<NoValue>> AddStudetRole(Guid id);
        Task<IResponse<NoValue>> AddLibrarinaRole(Guid id);
        Task<IResponse<NoValue>> RemoveRoles(Guid id);
    }
}

