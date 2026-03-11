using KAshop.BLL.Service;
using KAshop.DAL;
using KAshop.DAL.Data;
using KAshop.DAL.DTO.Request;
using KAshop.DAL.DTO.Response;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using KAshop.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace KAshop.PL.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _ICategoryService;

        public CategoryController(ICategoryService ICategoryService, IStringLocalizer<SharedResources> localizer)
        {
            _ICategoryService = ICategoryService;  
            _localizer = localizer;
        }


        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> create(CategoryRequest request )
        {
            var response = await _ICategoryService.CreateCategory(request);

            return Ok( new
            {
                message = _localizer["Success"].Value, 
                response
                
            } );
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> index()
        {
            var Categories = await _ICategoryService.GetAllCategories();
            
            return Ok(new
            {
                data = Categories,
                _localizer["Success"].Value
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _ICategoryService.GetCategory(c  => c.Id == id);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var deleted = await _ICategoryService.deleteCategory(id);

            if (!deleted)
            {
                return NotFound(new { message = _localizer["Not Found"].Value });
            }
            return Ok(new { message = _localizer["Success"].Value });
        }


    }
}
