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
    public class FavoriteItemRepository : IFavoriteItemRepository
    {
        private readonly ApplicationDbContext context;

        public FavoriteItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(FavoriteItem item)
        {
            context.favoriteItems.Add(item);
        }

        public void Delete(FavoriteItem item)
        {
            context.favoriteItems.Remove(item);
        }

        public List<FavoriteItem> GetAll()
        {
            return context.favoriteItems.ToList();
        }

        public FavoriteItem GetOne(Expression<Func<FavoriteItem, bool>> filter)
        {
           return context.favoriteItems.Where(filter).FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(FavoriteItem item)
        {
           context.favoriteItems.Update(item);
        }
    }
}
