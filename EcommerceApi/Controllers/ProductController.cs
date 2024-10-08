using data.Repository;
using data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
       
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
                    Image=item.Image,
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
        public IActionResult Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                Product product =new Product()
                {
                    Id = productDTO.Id,
                    Name = productDTO.Name,
                    CategoryId = productDTO.CategoryId,
                    Description = productDTO.Description,
                    Image=productDTO.Image,
                    Price = productDTO.Price,
                    Quantity = productDTO.Quantity,
                    Stock = productDTO.Stock,
                };
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
                Image = res.Image,
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
        public IActionResult EditProduct(int Id,ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = productRepository.GetOne(t => t.Id == Id);

                product.Id = productDTO.Id;
                product.Name = productDTO.Name;
                product.CategoryId = productDTO.CategoryId;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Quantity = productDTO.Quantity;
                product.Stock = productDTO.Stock;
                product.Image = productDTO.Image;

                productRepository.UpdateProduct(product);
                productRepository.Save();
                return Ok(product);
            }
            else
            {
                return BadRequest();
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
                    Image = product.Image,
                    CategoryName=product.Category.Name,
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
    }

}
