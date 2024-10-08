using data.Repository;
using data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository )
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var category =categoryRepository.GetAllCategory();
            List<CategoryDTO> categoryList = new List<CategoryDTO>();
            foreach (var item in category)
            {
                categoryList.Add(new CategoryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image=item.ImageUrl,
                    
                });
            }
            if (category == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(categoryList);
            }
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Id=categoryDTO.Id,
                    Name = categoryDTO.Name,
                    ImageUrl=categoryDTO.Image,
                };
                categoryRepository.Create(category);
                categoryRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOneCategory(int Id)
        {
            var result = categoryRepository.GetOne(t => t.Id == Id);
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id=result.Id,
                Name = result.Name,
                Image=result.ImageUrl,
            };
            if (result == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(categoryDTO);
            }
        }
        [HttpPut("{Id}")]
        public IActionResult EditCategory(int Id, CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var result = categoryRepository.GetOne(t => t.Id == Id);
                result.Id = categoryDTO.Id;
                result.Name = categoryDTO.Name;
                result.ImageUrl = categoryDTO.Image;

                categoryRepository.Update(result);
                categoryRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public IActionResult DeleteCategory(int Id)
        {
            var result = categoryRepository.GetOne(t => t.Id == Id);
            categoryRepository.Delete(result);
            categoryRepository.Save();
            return Ok();
        }
    }
}
