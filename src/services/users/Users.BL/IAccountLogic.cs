using Helpers.Models;
using Users.Model.Dtos;

namespace Users.BL
{
    public interface IAccountLogic
    {
        ResponseDetailsList<UserDto> GetAllUsers();

        ResponseDetailsBase AddNewUser(UserDto user);
    }
}