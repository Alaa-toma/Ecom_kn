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
    // مسؤول عن التعامل مع الداتا بيس 

    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
       public CategoryRepository(ApplicationDbContext context):base(context)
        {
        }
      
    }
}
