using Auction.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Services
{
    public class CategoriesService
    {

        public List<Auction.Entities.Category> GetAllCategories()
        {
            AuctionContext context = new AuctionContext();

            return context.Categories.ToList();
        }
        public Auction.Entities.Category GetCategoryByID(int ID)
        {
            AuctionContext context = new AuctionContext();

            return context.Categories.Find(ID);
        }
        public void SaveCategory(Auction.Entities.Category category)
        {
            AuctionContext context = new AuctionContext();

            context.Categories.Add(category);

            context.SaveChanges();
        }

        public void UpdateCategory(Auction.Entities.Category category)
        {
            AuctionContext context = new AuctionContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteCategory(Auction.Entities.Category category)
        {
            AuctionContext context = new AuctionContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
        }
    }

}
