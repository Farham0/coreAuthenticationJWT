using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using WebApplication4.Data.Model;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data
{
    public class SeadDataBase
    {
        public static void Initial(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicatioDbContext>();

            var userManager = serviceProvider.GetRequiredService < UserManager<ApplicationUser >> ();

            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {

                ApplicationUser us = new ApplicationUser()
                {
                    Email = "A@b.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "Ali",
                 

            };
                // Password should be complex enough to save :) 
                userManager.CreateAsync(us, "passWord!@1234");


            }
        }
    }
}
