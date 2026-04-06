using System.ComponentModel.DataAnnotations;

namespace LearningAPI.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<LessonDto> Lessons { get; set; } = new();
}

public class CreateCourseDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = string.Empty;
}

public class LessonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CourseId { get; set; }
}

public class CreateLessonDto
{
    [Required(ErrorMessage = "Lesson name is required")]
    public string Name { get; set; } = string.Empty;
}

public class EnrollDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CourseId { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
