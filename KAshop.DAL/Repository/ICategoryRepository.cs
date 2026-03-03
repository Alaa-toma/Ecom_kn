using KAshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{// اجبار كل من يتعامل مع الكاتيجوري بالالتزام بنفس الصيغة الموجودة في الانترفيس    
    public interface ICategoryRepository : IGenericRepositiory<Category>
    {
        // ما في داعي نكتب الكود هنا, ياخذه مباشرة من الجنريك
    }
}
