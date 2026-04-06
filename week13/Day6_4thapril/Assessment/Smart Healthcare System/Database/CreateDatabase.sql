-- =============================================
-- Smart Hospital Management System
-- Database Script
-- =============================================

CREATE DATABASE SmartHealthcareDB;
GO

USE SmartHealthcareDB;
GO

-- Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) CHECK (Role IN ('Admin','Doctor','Patient')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Departments Table
CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY,
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

-- Doctors Table
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY IDENTITY,
    UserId INT UNIQUE,
    DepartmentId INT,
    Specialization NVARCHAR(100),
    ExperienceYears INT,
    Availability NVARCHAR(100),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);

-- Appointments Table
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY,
    PatientId INT,
    DoctorId INT,
    AppointmentDate DATETIME,
    Status NVARCHAR(20) CHECK (Status IN ('Booked','Completed','Cancelled')),
    FOREIGN KEY (PatientId) REFERENCES Users(UserId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);

-- Prescriptions Table
CREATE TABLE Prescriptions (
    PrescriptionId INT PRIMARY KEY IDENTITY,
    AppointmentId INT UNIQUE,
    Diagnosis NVARCHAR(255),
    Medicines NVARCHAR(MAX),
    Notes NVARCHAR(255),
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId)
);

-- Bills Table
CREATE TABLE Bills (
    BillId INT PRIMARY KEY IDENTITY,
    AppointmentId INT,
    ConsultationFee DECIMAL(10,2),
    MedicineCharges DECIMAL(10,2),
    TotalAmount AS (ConsultationFee + MedicineCharges),
    PaymentStatus NVARCHAR(20) CHECK (PaymentStatus IN ('Paid','Unpaid')),
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId)
);

-- Sample Data
INSERT INTO Departments VALUES ('Cardiology','Heart related treatments');
INSERT INTO Departments VALUES ('Neurology','Brain and nerve treatments');
INSERT INTO Departments VALUES ('Orthopedics','Bone and joint treatments');
GO
