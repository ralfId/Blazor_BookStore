using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
    }
}
