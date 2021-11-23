using Auction.Data;
using Auction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Services
{
    public class BidsService
    {
        AuctionContext context = new AuctionContext();

        public bool AddBid(Bid bid)
        {

            context.Bids.Add(bid);
            return context.SaveChanges() > 0;
        }
    }

}
