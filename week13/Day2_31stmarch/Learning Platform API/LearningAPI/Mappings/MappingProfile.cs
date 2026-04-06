using AutoMapper;
using LearningAPI.DTOs;
using LearningAPI.Models;

namespace LearningAPI.Mappings;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Course, CourseDto>();
        CreateMap<Lesson, LessonDto>();
        CreateMap<CreateCourseDto, Course>();
        CreateMap<CreateLessonDto, Lesson>();
    }
}
