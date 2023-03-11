namespace Application.ViewModels;

public record UserViewModel(
    Guid Id,
    string Username,
    string Email,
    string DateBirth,
    string Gender,
    string Role);
