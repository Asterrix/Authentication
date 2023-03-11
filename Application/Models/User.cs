namespace Application.Models;

public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime DateBirth { get; init; }
    public Gender Gender { get; set; }
    public Role Role { get; set; } = Role.ContentCreator;
    public Active Active { get; set; } = Active.Offline;
    public DateTime TimeRegistration { get; init; } = DateTime.UtcNow;
    public DateTime LastVisit { get; set; } = DateTime.UnixEpoch;
}
