using _Services.Models.User;
using application.DataAccess;
using application.DataAccess.Models;

namespace _Services.EntityMapping
{
    public static class UserMapping
    {
        private static readonly AppDbContext _db = new AppDbContext();



        public static User User_CreateToUser(User_Create user)
        {
            return new User
            {
                F_Name = user.F_Name,
                L_Name = user.L_Name,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePicture = user.ProfilePicture
            };
        }

        public static User_Basic UserToUser_Basic(User user)
        {
            return new User_Basic
            {
                Id = user.Id,
                F_Name = user.F_Name,
                L_Name = user.L_Name,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePicture = user.ProfilePicture,

                PropertiesId = user.Properties.Select(i => i.Id).ToList(),
                InquiriesId = user.Inquiries.Select(i => i.Id).ToList(),
                FavoriteId = user.Favorite.Select(i => i.PropertyId).ToList()

                //Num_of_Properties = _db.Properties.Where(p => p.OwnerId == user.Id)?.Count() ?? 0,
                //Num_of_Inquiries = _db.Inquiries.Where(p => p.UserId == user.Id)?.Count() ?? 0
            };
        }

        public static User_Authenticate UserToUser_Authenticate(User user)
        {
            return new User_Authenticate
            {
                Id = user.Id,
                Full_Name = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePicture = user.ProfilePicture
            };
        }
    }
}
