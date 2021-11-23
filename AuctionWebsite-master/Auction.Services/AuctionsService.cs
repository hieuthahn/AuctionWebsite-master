using Auction.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Services
{
    public class AuctionsService
    {

        public List<Auction.Entities.Auction> GetAllAuctions()
        {
            AuctionContext context = new AuctionContext();

            return context.Auctions.ToList();
        }

        public List<Auction.Entities.Auction> SearchAuctions(int? categoryID, string searchTerm, int? pageNo, int pageSize)
        {
            AuctionContext context = new AuctionContext();
            var auctions = context.Auctions.AsQueryable();

            if(categoryID.HasValue && categoryID.Value > 0)
            {
                auctions = auctions.Where(x => x.CategoryID == categoryID.Value);
            }

            if(!string.IsNullOrEmpty(searchTerm))
            {
                auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            //pageNo = pageNo.HasValue ? pageNo.Value : 1;
            var skipCount = (pageNo.Value - 1) * pageSize;

            return auctions.OrderByDescending(x=>x.CategoryID).Skip(skipCount).Take(pageSize).ToList();
        }

        public List<Auction.Entities.Auction> GetPromotedAuctions()
        {
            AuctionContext context = new AuctionContext();

            return context.Auctions.Take(4).ToList();
        }

        public int GetAuctionCount(int? categoryID, string searchTerm)
        {
            AuctionContext context = new AuctionContext();

            var auctions = context.Auctions.AsQueryable();

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                auctions = auctions.Where(x => x.CategoryID == categoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            return auctions.Count();
        }

        public Auction.Entities.Auction GetAuctionByID(int ID)
        {
            AuctionContext context = new AuctionContext();

            return context.Auctions.Find(ID);
        }
        public void SaveAuction(Auction.Entities.Auction auction)
        {
            AuctionContext context = new AuctionContext();

            context.Auctions.Add(auction);

            context.SaveChanges();
        }

        public void UpdateAuction(Auction.Entities.Auction auction)
        {
            AuctionContext context = new AuctionContext();

            var existingAuction = context.Auctions.Find(auction.ID);

            context.AuctionPictures.RemoveRange(existingAuction.AuctionPictures);
            context.Entry(existingAuction).CurrentValues.SetValues(auction);

            context.AuctionPictures.AddRange(auction.AuctionPictures);

            context.SaveChanges();
        }

        public void DeleteAuction(Auction.Entities.Auction auction)
        {
            AuctionContext context = new AuctionContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
        }

        /*public void UpdateBidAuction(Auction.Entities.Auction auction)
        {
            AuctionContext context = new AuctionContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }*/
    }

}
