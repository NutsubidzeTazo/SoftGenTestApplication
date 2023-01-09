using Microsoft.EntityFrameworkCore;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoftGen.Repository.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly SoftGenApplicationDbContext _context;
        public BaseRepository(SoftGenApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task DeleteAsync(T entity)
        {
            //_context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> GetAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public abstract Task<IEnumerable<T>> SearchAsync(string? searchString);

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}