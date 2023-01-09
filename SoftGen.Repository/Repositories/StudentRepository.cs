using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        private readonly SoftGenApplicationDbContext _context;
        public StudentRepository(SoftGenApplicationDbContext context) :base(context)
        {
            _context = context;
        }


        public  override async  Task<IEnumerable<Student>> SearchAsync(string? searchString)
        {
            var query = _context.Students.AsNoTracking().Where(c => c.IsDeleted == false);
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
