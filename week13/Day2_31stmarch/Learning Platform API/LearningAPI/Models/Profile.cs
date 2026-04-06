namespace LearningAPI.Models;

public class Profile
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}