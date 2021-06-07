using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_ProfileService.Data
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {

            if (!Database.IsInMemory())
            {
                Console.WriteLine(Database.GetDbConnection().ConnectionString);
                Database.EnsureCreated();
            }
               
        }


        public DbSet<Profile> Profiles { get; set; }
   
    }
}
