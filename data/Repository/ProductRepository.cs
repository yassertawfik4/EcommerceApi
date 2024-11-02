using data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public List<Product> GetAllProductByCategory(int id)
        {
          return context.Products.Include(t=>t.Category).Where(t=>t.CategoryId==id).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.Include(p => p.Category).ToList();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            return context.Products.Include(p => p.Category).Where(filter).FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Update(product);
        }
        public List<Product> GetAllProductByCategoryName(string name)
        {
            return context.Products.Where(t => t.Category.Name == name).ToList();
        }
    }
}
