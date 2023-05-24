using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Security
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityEmployee,
        AppIdentityRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {

        }
    }
}
