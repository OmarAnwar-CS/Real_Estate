using _Services.Contracts;
using _Services.EntityMapping;
using _Services.Models.Property;
using _Services.Models.User;
using application.DataAccess.Models;
using Application.DataAccessContracts;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyService _propertyService;

        public UserService(IUnitOfWork unitOfWork, IPropertyService propertyService)
        {
            _unitOfWork = unitOfWork;
            _propertyService = propertyService;
        }


        public User_Basic GetUserById(int id)
        {

            try
            {
                var user = _unitOfWork.User.GetById(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                return UserMapping.UserToUser_Basic(user);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving the user.", ex);
            }
        }

        public User_Basic GetUserByEmail(string email)
        {

            try
            {
                var user = _unitOfWork.User.GetByEmail(email);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                return UserMapping.UserToUser_Basic(user);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving the user.", ex);
            }
        }


        public void CreateUser(User_Create _user)
        {

            _user.Address = "Address is Empty";
            _user.ProfilePicture = "https://img.freepik.com/free-vector/blue-circle-with-white-user_78370-4707.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1728518400&semt=ais_hybrid";

            ValidateUser(_user);

            try
            {
                _user.Password = HashPassword(_user.Password);

                _unitOfWork.User.Insert(UserMapping.User_CreateToUser(_user));
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while creating the user.", ex);
            }
        }

        public void UpdateUser(int id, User_Update _user)
        {
            try
            {
                var User = _unitOfWork.User.GetById(id);
                if (User == null)
                    throw new KeyNotFoundException("User not found.");
                if (!string.IsNullOrEmpty(_user.F_Name))
                    User.F_Name = _user.F_Name;
                if (!string.IsNullOrEmpty(_user.L_Name))
                    User.L_Name = _user.L_Name;
                if (!string.IsNullOrEmpty(_user.Email))
                    User.Email = _user.Email;
                if (!string.IsNullOrEmpty(_user.PhoneNumber))
                    User.PhoneNumber = _user.PhoneNumber;
                if (!string.IsNullOrEmpty(_user.Address))
                    User.Address = _user.Address;
                if (!string.IsNullOrEmpty(_user.ProfilePicture))
                    User.ProfilePicture = _user.ProfilePicture;


                _unitOfWork.User.Update(User);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }

        public void UpdateUser(string email, User_Update _user)
        {
            try
            {
                var User = _unitOfWork.User.GetByEmail(email);
                if (User == null)
                    throw new KeyNotFoundException("User not found.");
                if (!string.IsNullOrEmpty(_user.F_Name))
                    User.F_Name = _user.F_Name;
                if (!string.IsNullOrEmpty(_user.L_Name))
                    User.L_Name = _user.L_Name;
                if (!string.IsNullOrEmpty(_user.Email))
                    User.Email = _user.Email;
                if (!string.IsNullOrEmpty(_user.PhoneNumber))
                    User.PhoneNumber = _user.PhoneNumber;
                if (!string.IsNullOrEmpty(_user.Address))
                    User.Address = _user.Address;
                if (!string.IsNullOrEmpty(_user.ProfilePicture))
                    User.ProfilePicture = _user.ProfilePicture;


                _unitOfWork.User.Update(User);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }
        public void DeleteUser(int id)
        {
            try
            {
                var user = _unitOfWork.User.GetById(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                _unitOfWork.User.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while deleting the user.", ex);
            }
        }

        public void DeleteUser(string email)
        {
            try
            {
                var user = _unitOfWork.User.GetByEmail(email);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                _unitOfWork.User.Delete(user.Id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while deleting the user.", ex);
            }
        }


        public User_Authenticate AuthenticateUser(string email, string password)
        {
            try
            {
                var user = _unitOfWork.User.GetAll()
                    .FirstOrDefault(u => u.Email == email && u.Password == HashPassword(password));

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
                return UserMapping.UserToUser_Authenticate(user);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while authenticating the user.", ex);
            }
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            try
            {
                var user = _unitOfWork.User.GetById(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                user.Password = HashPassword(newPassword);
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while updating the user's password.", ex);
            }
        }

        public IEnumerable<Property_GetAll_Func> GetUserProperties(int userId)
        {
            try
            {
                var user = _unitOfWork.User.GetById(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                return _propertyService.GetAllProperties()
                    .Where(p => p.OwnerId == userId)
                    .ToList();
            }
            catch (Exception ex)
            {
                // سجل الاستثناء
                throw new ApplicationException("An error occurred while retrieving user's properties.", ex);
            }
        }

        public IEnumerable<Property_GetAll_Func> GetUserFavoritesProperty(int userId)
        {
            try
            {
                var favorites = _unitOfWork.Favorite.GetAllProperies(userId);

                return PropertyMapping.MapToPropertyGetAll(favorites);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while retrieving user's favorites.", ex);
            }
        }

        // Private method to validate user data
        private void ValidateUser(User_Create user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(user.F_Name))
                throw new ArgumentException("User first name is required.", nameof(user.F_Name));

            if (string.IsNullOrEmpty(user.L_Name))
                throw new ArgumentException("User last name is required.", nameof(user.L_Name));

            if (string.IsNullOrEmpty(user.PhoneNumber))
                throw new ArgumentException("User phone number is required.", nameof(user.PhoneNumber));

            if (user.PhoneNumber.Length != 11)
                throw new ArgumentException("User phone number must be 11 digits.", nameof(user.PhoneNumber));

            if (user.PhoneNumber[0] != '0' || user.PhoneNumber[1] != '1')
                throw new ArgumentException("User phone number must start with 01.", nameof(user.PhoneNumber));

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("User email is required.", nameof(user.Email));

            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("User password is required.", nameof(user.Password));

            if (user.Password.Length < 6)
                throw new ArgumentException("User password must be at least 6 characters.", nameof(user.Password));

            if (_unitOfWork.User.GetAll().Any(u => u.Email == user.Email))
                throw new ArgumentException("User email is already registered.", nameof(user.Email));

            if (!string.IsNullOrEmpty(user.Address) && user.Address.Length > 100)
                user.Address = "Address is Empty";

            if (!string.IsNullOrEmpty(user.ProfilePicture) && user.ProfilePicture.Length > 100)
                user.ProfilePicture = "https://img.freepik.com/free-vector/blue-circle-with-white-user_78370-4707.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1728518400&semt=ais_hybrid";

        }

        // Private method to hash passwords
        private string HashPassword(string password)
        {
            // Implement password hashing logic here
            return password; // Replace this with actual hashing implementation
        }
    }
}
