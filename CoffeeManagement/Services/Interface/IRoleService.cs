﻿using CoffeeManagement.Model;
using CoffeeManagement.ResponseModel.Role;

namespace CoffeeManagement.Services.Interface
{
    public interface IRoleService
    {
        public  Task<IEnumerable<RoleResponse>> GetAllRole();
        public  Task<RoleResponse> GetRoleById(Role.RoleType roleId);
    }
}
