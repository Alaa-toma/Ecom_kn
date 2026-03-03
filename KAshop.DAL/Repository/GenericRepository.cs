using KAshop.DAL.Data;
using KAshop.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepositiory<T> where T :class
    {

        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> createAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {

            //Include(c => c.Translations).ToListAsync();

            //هنا هو يحدد نوع t 
            return await _context.Set<T>().ToListAsync();
        }

    }
}
