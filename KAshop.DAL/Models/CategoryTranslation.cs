using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Models
{
    public class CategoryTranslation
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Language { get; set; } = "en";

        public int category_id { get; set; }
        public Category category { get; set; } = null!;

    }
}
