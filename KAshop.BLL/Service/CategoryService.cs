using KAshop.DAL.DTO.Request;
using KAshop.DAL.DTO.Response;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            // اي شرط او فحص للبيانات يتم هنا 
            var category = request.Adapt<Category>();
           await _ICategoryRepository.createAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _ICategoryRepository.GetAllAsync();
            return categories.Adapt< List<CategoryResponse> >(); 
        }
    }
}
