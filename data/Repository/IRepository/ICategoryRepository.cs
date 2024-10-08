using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository.IRepository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();
        Category GetOne(Expression<Func<Category, bool>> filter);
        void Update(Category category);
        void Delete(Category category);
        void Create(Category category);
        void Save();
    }
}
