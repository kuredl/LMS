using LMS.Server.Models;
using LMS_Server.DAL;
using LMS_Server.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LMS.Server.Controllers
{
    public class AdminController : ApiController

    {
        private ApplicationContext db = new ApplicationContext();

        public async Task<IHttpActionResult> GetRoles()
        {
            return Ok(db.Roles.ToList());
        }
        
        //[Authorize(Roles="administrator")]
        public async Task<IHttpActionResult> GetAllUsers()
        {

            return Ok(db.Users.ToList());
        }

        public async Task<IHttpActionResult> GetUserById(string Id)
        {
            return Ok(db.Users.Find(Id));
        }


        public async Task<IHttpActionResult> GetAllCourses()
        {
            return Ok(db.Courses.OrderBy( c=> c.CourseSubject).ToList());
        }

        public async Task<IHttpActionResult> GetCourseById(string Id)
        {
            return Ok(db.Courses.Find(Id));
        }


        [HttpPost]
        public async Task<IHttpActionResult> DeleteCourse(Course course)
        {
            var itemToRemove = db.Courses.Find(course.CourseId); //returns a single item.

                db.Courses.Remove(itemToRemove);
                db.SaveChanges();
                return Ok(db.Courses.ToList());


        }

        public async Task<IHttpActionResult> GetCoursesByUserId(string Id)
        {

            return Ok(db.Courses.Where(ui =>ui.Attendants == Id.ToList()));
        }

        public async Task<IHttpActionResult> GetUsersByCourseId(int Id)
        {


            return Ok(db.Courses.Where(u => u.CourseId == Id).Select(c => c.Attendants));
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNewCourse(Course co)
        {
            db.Courses.Add(co);
            db.SaveChanges();

            return Ok(db.Courses.ToList()); 
            
        }

        [HttpGet]
        public async Task<IHttpActionResult> AddUserToCourse(int cId, string uId)
        {
            
            var course = db.Courses.Find(cId);
            var user = db.Users.Find(uId);
            if (user == null || course == null)
            {
                return NotFound();
            }

            course.Attendants.Add(user);
            db.SaveChanges();
            return Ok();
           
        }

        //[HttpGet]
        //public async Task<IHttpActionResult> AddUserToRole(string rId, string uId)
        //{

        //    //var roleStore = new RoleStore<IdentityRole>(db);
        //    //var roleManager = new RoleManager<IdentityRole>(roleStore);
        //    //var userStore = new UserStore<ApplicationUser>(db);
        //    //var userManager = new UserManager<ApplicationUser>(userStore);

        //    var role = db.Roles.Find(rId);
        //    var user = db.Users.Find(uId);
        //    if (user == null || role == null)
        //    {
        //        return NotFound();
        //    }

        //    //userManager.AddToRole(user, role);

        //    role.Users.Add(user);
        //    db.SaveChanges();
        //    return Ok();

        //}


    }
}
