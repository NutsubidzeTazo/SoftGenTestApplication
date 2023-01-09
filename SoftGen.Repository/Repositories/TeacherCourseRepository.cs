using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class TeacherCourseRepository : BaseRepository<TeacherCourse>
    {
        private readonly SoftGenApplicationDbContext _context;
        public TeacherCourseRepository(SoftGenApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<IEnumerable<TeacherCourse>> SearchAsync(string? searchString)
        {
            throw new NotImplementedException();
        }
    }
}
