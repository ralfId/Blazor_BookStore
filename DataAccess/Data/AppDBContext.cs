using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<BookOrderDetails> BookOrderDetails { get; set; }
    }
}
