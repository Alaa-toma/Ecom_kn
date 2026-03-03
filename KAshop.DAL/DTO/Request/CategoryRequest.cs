using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.DTO.Request
{
    public class CategoryRequest
    {

        public List<CategoryTranslation> Translations { get; set; } = null!;
       
    }
}
