namespace LMS_Server.Migrations
{
    using LMS_Server.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_Server.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS_Server.DAL.ApplicationContext context)
        {
            
            var role_elev = new IdentityRole("Elev");
            var role_lärare = new IdentityRole("Lärare");
            context.Roles.AddOrUpdate(
                role_elev,
                role_lärare
                );

            var userStore   = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user1       = new ApplicationUser { UserName = "user1", Email = "test@test.test" };
            var user2       = new ApplicationUser { UserName = "user2", Email = "test2@test.test" };
            
            userManager.Create(user1, "asdf");
            userManager.Create(user2, "asdf");

            var folder2 = new Folder {
                Name = "Folder2",
                
            };
            var folder1 = new Folder {
                Name = "Folder1",
                MySubFolders = { folder2 },
            };
            user1.MyFolder = folder1;

            context.Users.AddOrUpdate(user2, user1);
            context.Folders.AddOrUpdate(folder1, folder2);
            context.SaveChanges();
        }
    }
}
