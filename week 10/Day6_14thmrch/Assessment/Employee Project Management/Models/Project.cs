using System.Collections.Generic;

namespace EmployeeProjectManagement.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}