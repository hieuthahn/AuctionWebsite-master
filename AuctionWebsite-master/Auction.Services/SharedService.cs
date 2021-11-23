using Auction.Data;
using Auction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Services
{
    public class SharedService
    {
        public int SavePicture(Auction.Entities.Picture picture)
        {
            AuctionContext context = new AuctionContext();

            context.Pictures.Add(picture);

            context.SaveChanges();

            return picture.ID;
        }

        public bool LeaveComment(Auction.Entities.Comment comment)
        {
            AuctionContext context = new AuctionContext();

            context.Comments.Add(comment);

            return context.SaveChanges() > 0;
        }

        public List<Comment> GetComments(int entityID, int recordID)
        {
            AuctionContext context = new AuctionContext();

            return context.Comments.Where(x => x.EntityID == entityID && x.RecordID == recordID).ToList();
        }
    }
}
