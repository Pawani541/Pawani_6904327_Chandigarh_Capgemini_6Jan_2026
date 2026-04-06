using System.ComponentModel.DataAnnotations;

namespace UniversityManagement.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; }

        public int Credits { get; set; }
    }
}