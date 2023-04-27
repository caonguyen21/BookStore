namespace BookStore.Models;

public partial class Employee
{
    public int EmployeesId { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public byte[]? Image { get; set; }
}
