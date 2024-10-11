using API_Project.DataAccess.Models;
using API_Project.DataAccessContracts;
using application.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace API_Project.DataAccess.Repositories
{
    internal class InquiryRepository : BaseRepository<Inquiry>, IInquiryRepository
    {
        public InquiryRepository(AppDbContext context) : base(context)
        {
        }
        public override IEnumerable<Inquiry> GetAll()
        {
            return _dbSet.Include(i => i.User)
                         .Include(i => i.Property)
                         .AsEnumerable();
        }

    }
}
