using Application.Models;
using Application.ViewModels;

namespace Application.Mapper;
public static class ModelMapper
{
    public static void MapToUserViewModel(ref User user, out UserViewModel model)
    {
        var mappedUser = new UserViewModel(
            user.Id,
            user.Username,
            user.Email,
            user.DateBirth.ToShortDateString(),
            user.Gender.ToString(),
            user.Role.ToString());

        model = mappedUser;
    }
}
