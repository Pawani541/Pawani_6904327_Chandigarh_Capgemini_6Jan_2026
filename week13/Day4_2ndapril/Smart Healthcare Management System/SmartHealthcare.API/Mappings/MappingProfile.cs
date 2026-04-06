using AutoMapper;
using SmartHealthcare.Core.DTOs;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<CreatePatientDto, Patient>();

            CreateMap<Doctor, DoctorDto>()
                .ForMember(d => d.Specializations, opt => opt.MapFrom(src =>
                    src.DoctorSpecializations.Select(ds => ds.Specialization.Name).ToList()));
            CreateMap<CreateDoctorDto, Doctor>();

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(d => d.DoctorName,  opt => opt.MapFrom(src => src.Doctor.FullName));
            CreateMap<CreateAppointmentDto, Appointment>();
        }
    }
}
