using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Server.Models
{
    public enum LessonTime { AM, PM }

    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Week { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public LessonTime LessonTime { get; set; }  //Only AM or PM

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        //public virtual ICollection<ApplicationUser> Attendants { get; set; }

    }

}
