using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHealthcare.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSymptomsAndPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "HospitalPrescriptions");

            migrationBuilder.AddColumn<decimal>(
                name: "MedicineCharges",
                table: "HospitalPrescriptions",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                table: "HospitalAppointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineCharges",
                table: "HospitalPrescriptions");

            migrationBuilder.DropColumn(
                name: "Symptoms",
                table: "HospitalAppointments");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "HospitalPrescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
