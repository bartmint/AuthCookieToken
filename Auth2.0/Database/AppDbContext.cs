using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2._0.Database
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var haszer = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>()
                 .HasData(
                 new ApplicationUser
                 {
                     Id = Guid.NewGuid().ToString(),
                     UserName = "bartek@gmail.com",
                     NormalizedUserName = "bartek@gmail.com".ToUpper(),
                     Email = "bartek@gmail.com",
                     NormalizedEmail = "bartek@gmail.com".ToUpper(),
                     EmailConfirmed = true,
                     PasswordHash = haszer.HashPassword(null, "admin"),
                     SecurityStamp = string.Empty
                 }) ;
        }
    }
}
