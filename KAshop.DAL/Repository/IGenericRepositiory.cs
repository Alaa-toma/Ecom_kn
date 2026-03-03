using KAshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{
    public interface IGenericRepositiory <T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task<T> createAsync(T category);


    }
}
