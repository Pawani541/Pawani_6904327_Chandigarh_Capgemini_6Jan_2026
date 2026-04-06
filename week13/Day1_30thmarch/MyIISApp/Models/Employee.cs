public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }   // ← add required
    public required string Email { get; set; }  // ← add required
}