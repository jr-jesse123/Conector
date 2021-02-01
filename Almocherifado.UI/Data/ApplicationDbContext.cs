using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Almocherifado.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hahser = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@admin",
                NormalizedUserName = "admin@admin",
                Email = "user@admin",
                NormalizedEmail = "user@admin",
                EmailConfirmed = true,
                PasswordHash = hahser.HashPassword(null, "SenhaAdmin"),
                SecurityStamp = string.Empty
            });

            //builder.Entity<IdentityUser>().HasData(user);
        }
    }
}
