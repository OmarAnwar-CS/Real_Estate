using _Services.Models.Property;
using _Services.Models.User;

namespace _Services.Contracts
{
    public interface IUserService
    {
        void CreateUser(User_Create user);
        void UpdateUser(int id, User_Update user);
        void UpdateUser(string email, User_Update user);
        User_Basic GetUserById(int id);
        User_Basic GetUserByEmail(string email);
        void DeleteUser(int id);
        void DeleteUser(string email);
        User_Authenticate AuthenticateUser(string email, string password);
        void UpdateUserPassword(int userId, string newPassword);
        IEnumerable<Property_GetAll_Func> GetUserProperties(int userId);
        IEnumerable<Property_GetAll_Func> GetUserFavoritesProperty(int userId);
    }
}
