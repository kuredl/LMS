namespace LMS_Server.Migrations
{
    using LMS.Server.Models;
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
            //Seeda en ny user och samtidigt skapa och tilldela en roll
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            if (!context.Users.Any(u => u.UserName == "student1@test.se"))
            {

                var user = new ApplicationUser { UserName = "student1@test.se", Email = "student1@test.se" };

                userManager.Create(user, "Qrt55%");
                roleManager.Create(new IdentityRole { Name = "student" });
                userManager.AddToRole(user.Id, "student");
            }
            if (!context.Users.Any(u => u.UserName == "student2@test.se"))
            {

                var user = new ApplicationUser { UserName = "student2@test.se", Email = "student2@test.se" };

                userManager.Create(user, "Qrt55%");
                roleManager.Create(new IdentityRole { Name = "student" });
                userManager.AddToRole(user.Id, "student");
            }

            if (!context.Users.Any(u => u.UserName == "teacher1@test.se"))
            {

                var user = new ApplicationUser { UserName = "teacher1@test.se", Email = "teacher1@test.se" };

                userManager.Create(user, "Qrt55%");
                roleManager.Create(new IdentityRole { Name = "teacher" });
                userManager.AddToRole(user.Id, "teacher");
            }
            if (!context.Users.Any(u => u.UserName == "admin@test.se"))
            {

                var user = new ApplicationUser { UserName = "admin@test.se", Email = "admin@test.se" };

                userManager.Create(user, "Qrt55%");
                roleManager.Create(new IdentityRole { Name = "administrator" });
                userManager.AddToRole(user.Id, "administrator");
            }


            context.Courses.AddOrUpdate(
                new Course { CourseId = 1, CourseSubject = "English A1" },
                new Course { CourseId = 2, CourseSubject = "English A2" },
                new Course { CourseId = 3, CourseSubject = "English A3" },
                new Course { CourseId = 4, CourseSubject = "French A1" },
                new Course { CourseId = 5, CourseSubject = "French A2" },
                new Course { CourseId = 6, CourseSubject = "French A3" },
                new Course { CourseId = 7, CourseSubject = "Swedish A1" },
                new Course { CourseId = 8, CourseSubject = "Swedish A2" },
                new Course { CourseId = 9, CourseSubject = "Swedish A3" },
                new Course { CourseId = 10, CourseSubject = "Rubbish One-on-One" }
                );
            context.Lessons.AddOrUpdate(
                new Lesson { Id = 1, Description = "", Week = 34, DayOfWeek = 0, LessonTime = 0, CourseId = 1 }
                );
            context.SaveChanges();


            
            //var role_elev = new IdentityRole("Elev");
            //var role_lärare = new IdentityRole("Lärare");
            //context.Roles.AddOrUpdate(
            //    role_elev,
            //    role_lärare
            //    );

            //var userStore   = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user1       = new ApplicationUser { UserName = "user1", Email = "test@test.test" };
            //var user2       = new ApplicationUser { UserName = "user2", Email = "test2@test.test" };
            
            //userManager.Create(user1, "asdf");
            //userManager.Create(user2, "asdf");

            //var folder2 = new Folder {
            //    Name = "Folder2",
                
            //};
            //var folder1 = new Folder {
            //    Name = "Folder1",
            //    MySubFolders = { folder2 },
            //};
            //user1.MyFolder = folder1;

            //context.Users.AddOrUpdate(user2, user1);
            //context.Folders.AddOrUpdate(folder1, folder2);
            //context.SaveChanges();
        }
    }
}
