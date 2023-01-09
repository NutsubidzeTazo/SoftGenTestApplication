using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class StudentCourseRepository : BaseRepository<StudentCourse>
    {
        private readonly SoftGenApplicationDbContext _context;
        public StudentCourseRepository(SoftGenApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public override Task<IEnumerable<StudentCourse>> SearchAsync(string? searchString)
        {
            throw new NotImplementedException();
        }
    }
}
