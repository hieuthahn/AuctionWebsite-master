using Auction.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.Web.ViewsModels
{
    public class AuctionsListingViewModels : PageViewModels
    {
        public List<Auction.Entities.Auction> Auctions { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public int? PageNo { get; set; }
        public List<Category> Categories { get; set; }

        public List<Auction.Entities.Auction> AllAuctions { get; set; }
        public List<Auction.Entities.Auction> PromotedAuctions { get; set; }

    }
    public class AuctionsViewModels : PageViewModels
    {
        public List<Auction.Entities.Auction> AllAuctions { get; set; }
        public List<Auction.Entities.Auction> PromotedAuctions { get; set; }
    }

    public class AuctionsDetailsViewModels : PageViewModels
    {
        public Auction.Entities.Auction Auction { get; set; }
        public List<Comment> Comments { get; set; }
        public decimal BidsAmount { get; set; }
        public AuctionUser LatesBidder { get; set; }
        public int EntityID { get; set; }
    }

    public class CreateAuctionViewModels : PageViewModels
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Minimum length should be 4 characters.")] //nvarchar
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(10, 1000000, ErrorMessage = "Actual Amount must be within 100 - 1000000.")]
        public decimal ActualAmount { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public string AuctionPictures { get; set; }

        public List<Category> Categories { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
    }
}