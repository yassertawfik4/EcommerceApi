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
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Id=categoryDTO.Id,
                    Name = categoryDTO.Name,
                };
                if (categoryDTO.ImageFile != null && categoryDTO.ImageFile.Length > 0)
                {
                    // استخدم الاسم الأصلي للصورة
                    var fileName = Path.GetFileName(categoryDTO.ImageFile.FileName);

                    // حدد المسار لحفظ الصورة في wwwroot/Images
                    var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                    // تأكد من أن الدليل موجود
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    // تحديد المسار الكامل للصورة الجديدة
                    var filePath = Path.Combine(imagesDirectory, fileName);

                    // حفظ الملف
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await categoryDTO.ImageFile.CopyToAsync(stream);
                    }

                    // تعيين خاصية ImagePath للمنتج
                    category.ImageUrl = $"/src/assets/{fileName}";
                }

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
        [HttpGet("by/{name}")]

        public IActionResult GetOneCategoryByName(string name)
        {
            var result = categoryRepository.GetOne(t => t.Name == name);
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Image = result.ImageUrl,
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
        public async Task<IActionResult> EditCategory(int Id, CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = categoryRepository.GetOne(t => t.Id == Id);
            if (result == null)
            {
                return NotFound();
            }

            // Update basic properties
            result.Name = categoryDTO.Name;

            // Update image if a new file is provided
            if (categoryDTO.ImageFile != null && categoryDTO.ImageFile.Length > 0)
            {
                // استخدم الاسم الأصلي للصورة
                var fileName = Path.GetFileName(categoryDTO.ImageFile.FileName);

                // حدد المسار لحفظ الصورة في wwwroot/Images
                var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                // تأكد من أن الدليل موجود
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                // تحديد المسار الكامل للصورة الجديدة
                var filePath = Path.Combine(imagesDirectory, fileName);

                // حفظ الملف
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await categoryDTO.ImageFile.CopyToAsync(stream);
                }

                // تعيين خاصية ImagePath للمنتج
                result.ImageUrl = $"/src/assets/{fileName}";
            }
            categoryRepository.Update(result);
            categoryRepository.Save();
            return Ok();
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
