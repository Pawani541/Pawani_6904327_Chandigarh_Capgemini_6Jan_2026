using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext db)
        {
            // Seed Specializations
            if (!db.Specializations.Any())
            {
                db.Specializations.AddRange(
                    new Specialization { Name = "Cardiology" },
                    new Specialization { Name = "Dermatology" },
                    new Specialization { Name = "Neurology" },
                    new Specialization { Name = "Orthopedics" },
                    new Specialization { Name = "General Medicine" }
                );
                db.SaveChanges();
            }

            // Seed Doctors
            if (!db.Doctors.Any())
            {
                var cardiology  = db.Specializations.First(s => s.Name == "Cardiology");
                var dermatology = db.Specializations.First(s => s.Name == "Dermatology");
                var neurology   = db.Specializations.First(s => s.Name == "Neurology");
                var orthopedics = db.Specializations.First(s => s.Name == "Orthopedics");
                var generalMed  = db.Specializations.First(s => s.Name == "General Medicine");

                db.Doctors.AddRange(
                    new Doctor
                    {
                        FullName = "Dr. Anil Sharma", Email = "anil.sharma@hospital.com",
                        Phone = "9876543210", ExperienceYears = 12,
                        DoctorSpecializations = new List<DoctorSpecialization>
                        { new DoctorSpecialization { SpecializationId = cardiology.Id } }
                    },
                    new Doctor
                    {
                        FullName = "Dr. Priya Mehta", Email = "priya.mehta@hospital.com",
                        Phone = "9876543211", ExperienceYears = 8,
                        DoctorSpecializations = new List<DoctorSpecialization>
                        { new DoctorSpecialization { SpecializationId = dermatology.Id } }
                    },
                    new Doctor
                    {
                        FullName = "Dr. Rajesh Kumar", Email = "rajesh.kumar@hospital.com",
                        Phone = "9876543212", ExperienceYears = 15,
                        DoctorSpecializations = new List<DoctorSpecialization>
                        { new DoctorSpecialization { SpecializationId = neurology.Id } }
                    },
                    new Doctor
                    {
                        FullName = "Dr. Sunita Verma", Email = "sunita.verma@hospital.com",
                        Phone = "9876543213", ExperienceYears = 10,
                        DoctorSpecializations = new List<DoctorSpecialization>
                        { new DoctorSpecialization { SpecializationId = orthopedics.Id } }
                    },
                    new Doctor
                    {
                        FullName = "Dr. Manish Gupta", Email = "manish.gupta@hospital.com",
                        Phone = "9876543214", ExperienceYears = 6,
                        DoctorSpecializations = new List<DoctorSpecialization>
                        { new DoctorSpecialization { SpecializationId = generalMed.Id } }
                    }
                );
                db.SaveChanges();
            }

            // Seed Admin User
            if (!db.Users.Any(u => u.Role == "Admin"))
            {
                db.Users.Add(new User
                {
                    Username     = "Admin",
                    Email        = "admin@healthcare.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role         = "Admin"
                });
                db.SaveChanges();
            }
        }
    }
}
