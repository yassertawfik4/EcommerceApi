using data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Category category)
        {
            context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            context.Categories.Remove(category);
        }

        public List<Category> GetAllCategory()
        {
            return context.Categories.ToList();
        }

        public Category GetOne(Expression<Func<Category, bool>> filter)
        {
            return context.Categories.Where(filter).FirstOrDefault();
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
