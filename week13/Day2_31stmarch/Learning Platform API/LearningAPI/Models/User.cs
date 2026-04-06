namespace LearningAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public Profile? Profile { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}