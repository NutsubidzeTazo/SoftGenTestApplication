using Microsoft.EntityFrameworkCore;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>
    {
        private readonly SoftGenApplicationDbContext _context;
        public TeacherRepository(SoftGenApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Teacher>> SearchAsync(string? searchString)
        {
            var query = _context.Teachers.AsNoTracking().Where(c => c.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.FirstName.Contains(searchString)
                                      || c.LastName.Contains(searchString)
                                      || c.PersonalNumber.Contains(searchString));
            }
            return await query.ToListAsync();
        }
    }
}
