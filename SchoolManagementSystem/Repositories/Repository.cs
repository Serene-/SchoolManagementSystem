using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SchoolContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(SchoolContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T Entity)
        {
           await _dbSet.AddAsync(Entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public void Remove(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }
    }
}
