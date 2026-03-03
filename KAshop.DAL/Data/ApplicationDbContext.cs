using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAshop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace KAshop.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> categoryTranslations { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base (options) 
        {
        }
    }
}
