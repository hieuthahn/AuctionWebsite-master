 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Entities;
using Microsoft.AspNet.Identity.EntityFramework;



namespace Auction.Data
{
    public class AuctionContext : IdentityDbContext<Auction.Entities.AuctionUser>
    {
        public AuctionContext() : base("name=AuctionConnectionString")
        {
            Database.SetInitializer<AuctionContext>(new AucionDBInitializer());
        }

        public DbSet<Auction.Entities.Category> Categories { get; set; }
        public DbSet<Auction.Entities.Auction> Auctions { get; set; }
        public DbSet<Auction.Entities.Picture> Pictures { get; set; }
        public DbSet<Auction.Entities.AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Auction.Entities.Bid> Bids { get; set; }
        public DbSet<Auction.Entities.Comment> Comments { get; set; }


        public static AuctionContext Create()
        {
            return new AuctionContext();
        }
    }
}
