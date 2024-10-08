using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository.IRepository
{
    public interface IProductRepository
    { 
        List<Product>  GetAllProducts();
        List<Product> GetAllProductByCategory(int id);
        Product GetOne(Expression<Func<Product, bool>> filter);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void Save();
    }
}
