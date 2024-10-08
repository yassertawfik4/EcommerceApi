using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository.IRepository
{
    public interface ICardItemRepository
    {
        List<CardItem> GetAll();
        CardItem GetOne(Expression<Func<CardItem,bool>> filter);
        void Update(CardItem cardItem);
        void Delete(CardItem cardItem);
        void Create(CardItem cardItem);
        void Save();
    }
}
