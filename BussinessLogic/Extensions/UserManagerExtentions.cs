using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.Extensions
{
    public static class Extention
    {
        public static User AndProjects(this UserManager<User> manager, string name) =>
            manager.Users
                .Where(u => u.UserName.Equals(name))
                .Include(p => p.Projects)
                .ThenInclude(t => t.Tasks)
                .ThenInclude(f => f.Fields)
                .Single();
    }
}
