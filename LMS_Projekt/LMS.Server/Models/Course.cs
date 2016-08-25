using LMS_Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Server.Models
{
    public class Course
    {

        [Key]
        public int CourseId { get; set; }
        public string CourseSubject { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<ApplicationUser> Attendants { get; set; }
    }
}
