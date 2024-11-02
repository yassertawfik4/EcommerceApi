using data.Repository;
using data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Models;
using Models.DTO;
using System.Collections.Generic;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment hostEnvironment)
        {
            this.productRepository = productRepository;
            this.hostEnvironment = hostEnvironment;
        }
        //display
        [HttpGet("AllProduct")]
        public IActionResult GetAllProducts()
        {
            var res = productRepository.GetAllProducts();
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var item in res) 
            {
                products.Add(new ProductDTO
                {
                    Id=item.Id,
                    Name=item.Name,
                    CategoryId=item.CategoryId,
                    Description=item.Description,
                    ImageUrl = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Stock = item.Stock,
                    CategoryName=item.Category.Name,

                });
            }
            if (res == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(products);
            }
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Id = productDTO.Id,
                    Name = productDTO.Name,
                    CategoryId = productDTO.CategoryId,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    Quantity = productDTO.Quantity,
                    Stock = productDTO.Stock,
                };

                if (productDTO.ImageFile != null && productDTO.ImageFile.Length > 0)
                {
                    // استخدم الاسم الأصلي للصورة
                    var fileName = Path.GetFileName(productDTO.ImageFile.FileName);

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
                        await productDTO.ImageFile.CopyToAsync(stream);
                    }

                    // تعيين خاصية ImagePath للمنتج
                    product.Image = $"/src/assets/{fileName}";
                }

                productRepository.AddProduct(product);
                productRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOneProduct(int Id) 
        {
            var res = productRepository.GetOne(t=>t.Id==Id);
            ProductDTO productDTO = new ProductDTO()
            {
                Id = res.Id,
                Name = res.Name,
                CategoryId = res.CategoryId,
                Description = res.Description,
                 CategoryName = res.Category != null ? res.Category.Name : null,
               ImageUrl=res.Image,
                Price = res.Price,
                Quantity = res.Quantity,
                Stock = res.Stock,


            };
            if (res == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(productDTO);

            }

        }
        [HttpPut("{Id}")]
        public IActionResult EditProduct(int Id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = productRepository.GetOne(t => t.Id == Id);

            if (product == null)
            {
                return NotFound($"Product with Id = {Id} not found.");
            }

            // Update product properties
            product.Name = productDTO.Name;
            product.CategoryId = productDTO.CategoryId;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Quantity = productDTO.Quantity;
            product.Stock = productDTO.Stock;
            product.Image = productDTO.ImageUrl;

            try
            {
                productRepository.UpdateProduct(product);
                productRepository.Save();
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Handle any exception that might occur during the update
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int Id)
        {
            var res = productRepository.GetOne(t=>t.Id==Id);
            productRepository.DeleteProduct(res);
            productRepository.Save();
            return Ok(res);
        }
        [HttpGet("category/{id}")] 
        public IActionResult GetProductByCategoryId(int id)
        {
            var products = productRepository.GetAllProductByCategory(id); 

           List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (var product in products) 
            {
                productDTOs.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Stock = product.Stock,
                    CategoryName=product.Category.Name,
                    ImageUrl=product.Image,
                });                
            }
            if (products == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(productDTOs);
            }

        }
        [HttpGet("category/name/{name}")]
        public IActionResult GetProductByCategoryName(string name)
        {
            var products = productRepository.GetAllProductByCategoryName(name);
           

            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (var product in products)
            {
                productDTOs.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Stock = product.Stock,
                    CategoryName = product.Category?.Name, // Use null-conditional operator to avoid null reference
                    ImageUrl = product.Image,
                });
            }

            return Ok(productDTOs);
        }
    }

}
