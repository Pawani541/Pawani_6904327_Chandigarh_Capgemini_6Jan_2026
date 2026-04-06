# Smart Hospital Management System
## Week 13 Assessment - Capgemini Chandigarh

### Tech Stack
- Frontend: ASP.NET Core MVC (Razor Views)
- Backend: ASP.NET Core Web API
- Database: SQL Server
- ORM: Entity Framework Core (Code First + Database First)

### Project Structure
SmartHealthcare.API/   -> Web API (Backend)
SmartHealthcare.MVC/   -> MVC App (Frontend)
Database/              -> SQL Scripts

### Database Setup
1. Open SQL Server Management Studio
2. Run Database/CreateDatabase.sql

### API Endpoints
| Method | URL                          | Description         |
|--------|------------------------------|---------------------|
| GET    | /api/users                   | Get all users       |
| POST   | /api/users/register          | Register user       |
| POST   | /api/users/login             | Login               |
| GET    | /api/doctors                 | Get all doctors     |
| GET    | /api/doctors/department/{id} | Doctors by dept     |
| GET    | /api/appointments            | All appointments    |
| POST   | /api/appointments            | Book appointment    |
| PUT    | /api/appointments/{id}/status| Update status       |
| GET    | /api/bills                   | All bills           |
| POST   | /api/bills                   | Create bill         |
| PUT    | /api/bills/{id}/pay          | Mark as paid        |

### Relationships
- User -> Doctor (One-to-One)
- Department -> Doctors (One-to-Many)
- Patient -> Appointments (One-to-Many)
- Doctor -> Appointments (One-to-Many)
- Appointment -> Prescription (One-to-One)
- Appointment -> Bill (One-to-One)
