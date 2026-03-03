using KAshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{
    public interface IGenericRepositiory <T> where T : class
    {

        Task<List<T>> GetAllAsync(string[]? includes = null);
        Task<T> createAsync(T category);

        Task<T> getone(Expression<Func<T, bool>> filter, string[]? includes = null);
    }
}
