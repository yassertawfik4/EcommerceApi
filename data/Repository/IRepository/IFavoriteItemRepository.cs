using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository.IRepository
{
    public interface IFavoriteItemRepository
    {
        List<FavoriteItem> GetAll();
        FavoriteItem GetOne(Expression<Func<FavoriteItem,bool>> filter);
        void Update(FavoriteItem item);
        void Delete(FavoriteItem item);
        void Create(FavoriteItem item);
        void Save();
    }
}
