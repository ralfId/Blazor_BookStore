using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TiendaProducto_Server.Services.IService;

namespace TiendaProducto_Server.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDBContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public DbInitializer(AppDBContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) //check for pending migrations
                {
                    _db.Database.Migrate(); //do pending migrations
                }

                CreateRoles();
                CreateUser();
                EnrollUser();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void EnrollUser()
        {
            var userAdmin = _db.Users.FirstOrDefault(x => x.Email == "ralf_raid@yopmail.com");
            _userManager.AddToRoleAsync(userAdmin, ConstantsCommon.Role_Admin).GetAwaiter().GetResult();
        }

        private void CreateUser()
        {
            if (!_db.Users.Any())
            {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "ralf_raid@yopmail.com",
                    Email = "ralf_raid@yopmail.com",
                    EmailConfirmed = true,
                }, "Admin123$").GetAwaiter().GetResult();
            }
        }

        private void CreateRoles()
        {
            if (!_db.Roles.Any())
            {
                 _roleManager.CreateAsync(new IdentityRole(ConstantsCommon.Role_Admin)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(ConstantsCommon.Role_Client)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(ConstantsCommon.Role_Employee)).GetAwaiter().GetResult();
            }
        }
    }
}
