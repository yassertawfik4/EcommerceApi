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
    public class CardItemRepository : ICardItemRepository
    {
        private readonly ApplicationDbContext context;

        public CardItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(CardItem cardItem)
        {
            context.CardItems.Add(cardItem);
        }

        public void Delete(CardItem cardItem)
        {
            context.CardItems.Remove(cardItem);
        }

        public List<CardItem> GetAll()
        {
           return context.CardItems.ToList();
        }

        public CardItem GetOne(Expression<Func<CardItem, bool>> filter)
        {
            return context.CardItems.Where(filter).FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(CardItem cardItem)
        {
            context.CardItems.Update(cardItem);
        }
    }
}
