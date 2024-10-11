using application.DataAccess;
using application.DataAccess.Models;
using Application.DataAccessContracts;
using Microsoft.EntityFrameworkCore;

namespace API_Project.DataAccess.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public override User? GetById(int Id)
        {
            return _dbSet.Include(user => user.Properties)
                         .Include(user => user.Inquiries)
                         .Include(user => user.Favorite)
                         .FirstOrDefault(User => User.Id == Id);
        }

        public User? GetByEmail(string email)
        {
            return _dbSet.Include(user => user.Properties)
                         .Include(user => user.Inquiries)
                         .Include(user => user.Favorite)
                         .FirstOrDefault(User => User.Email == email);
        }

        public User? GetByEmailAndPassword(string email, string password)
        {
            return _dbSet.FirstOrDefault(User => User.Email == email && User.Password == password);
        }

        public IEnumerable<Property> GetMyProperties(int UserId)
        {
            return _db.Properties.Include(p => p.Amenities).Include(p => p.Images).Include(p => p.Inquiries).Where(p => p.Owner.Id == 1).ToList();
        }

        public IEnumerable<Property?> GetMyFavoriteProperties(int UserId)
        {

            var properties = _db.Favorites.Where(f => f.UserId == UserId).Include(f => f.Property).Select(f => f.Property);

            properties = properties.Include(p => p.Images)
                                   .Include(p => p.Amenities);



            return properties.ToList();
        }
    }
}
