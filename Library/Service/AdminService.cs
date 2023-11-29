using Library.InterfaceService;
using Library.Models;
using Library.Models.UsersHelperModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class AdminService : IAdminService
    {
        private readonly LibraryDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminService(LibraryDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IResponse<List<UserModel>>> GetUsers()
        {
            var response = new Response<List<UserModel>>();
            try
            {
                var users = _userManager.Users.ToList();
                var model = users.Select(u => new UserModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.PhoneNumber
                }).ToList();

                foreach (var item in model)
                {
                    var user = users.Where(u => u.Id == item.Id).FirstOrDefault();
                    var role = await _userManager.GetRolesAsync(user); //sve role 
                    if (role.Any())
                    {
                        item.Role = role.FirstOrDefault();    
                    }
                }
                response.Value = model;
                response.Status = StatusEnum.Success;
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

        public async Task<IResponse<NoValue>> AddStudetRole(Guid id)
        {
            var response = new Response<NoValue>();
            try
            {
                var users = _userManager.Users.ToList();
                var user = users.Where(u => u.Id == id).FirstOrDefault();
                var test = await _userManager.AddToRoleAsync(user, "Student");
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

        public async Task<IResponse<NoValue>> AddLibrarinaRole(Guid id)
        {
            var response = new Response<NoValue>();
            try
            {
                var users = _userManager.Users.ToList();
                var user = users.Where(u => u.Id == id).FirstOrDefault();
                var test = await _userManager.AddToRoleAsync(user, "Librarian");
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

        public async Task<IResponse<NoValue>> RemoveRoles(Guid id)
        {
            var response = new Response<NoValue>();
            try
            {
                var users = _userManager.Users.ToList();
                var user = users.Where(u => u.Id == id).FirstOrDefault();
                var roles = await _userManager.GetRolesAsync(user); //sve role 
                await _userManager.RemoveFromRolesAsync(user, roles); //brise sve role 
                response.Status = StatusEnum.Success;
                response.Message = "Success";
            }
            catch(Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Message = "Something went Wrong";
            }
            return response;
        }

    }
}
