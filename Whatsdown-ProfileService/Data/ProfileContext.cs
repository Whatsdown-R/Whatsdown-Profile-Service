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
                if (Database.CanConnect())
                {
                    Console.WriteLine("Profile service can connect to database!!!");
                    Console.WriteLine("The database is named: " + Database.GetDbConnection().Database.ToString());
                }
                else
                {
                    Console.WriteLine("Profile service cannot connect to database!!!");
                }
               
                Database.EnsureCreated();
             
            }
               
        }


        public DbSet<Profile> Profiles { get; set; }
   
    }
}
