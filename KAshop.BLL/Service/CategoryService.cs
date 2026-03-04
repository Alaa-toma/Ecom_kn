using KAshop.DAL.DTO.Request;
using KAshop.DAL.DTO.Response;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _ICategoryRepository;
        public CategoryService(ICategoryRepository IcategRepository)
        {
            _ICategoryRepository = IcategRepository;
        }

        // انشاء كاتيجوري جديدة
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            // اي شرط او فحص للبيانات يتم هنا 
            var category = request.Adapt<Category>();
           await _ICategoryRepository.createAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        //يرجع كل الكاتيجوريز
        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _ICategoryRepository.GetAllAsync(new string[] { nameof(Category.Translations) });
            return categories.Adapt< List<CategoryResponse> >(); 
        }

        // يرجع كاتيجوري وحدة 
        public async Task<CategoryResponse?> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _ICategoryRepository.getone(filter, new string[] {nameof(Category.Translations)});
            return category.Adapt< CategoryResponse? >();   
        }

        public async Task<bool> deleteCategory(int id)
        {
            var client = await _ICategoryRepository.getone(i => i.Id == id);

            if (client == null) return false;  // العنصر مش موجود

            return await _ICategoryRepository.deleteAsync(client);

        }

    }
}
