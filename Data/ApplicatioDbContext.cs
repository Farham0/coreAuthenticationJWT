using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication4.Data
{
    public class ApplicatioDbContext:IdentityDbContext<Model.ApplicationUser>
    {
        public ApplicatioDbContext(DbContextOptions<ApplicatioDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
