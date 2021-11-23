using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Entities
{
    public class Auction : BaseEntity
    {
        public virtual Category Category { get; set; }
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
        public Nullable<DateTime> EndingTime { get; set; }

        public virtual List<AuctionPicture> AuctionPictures { get; set; }
        public virtual List<Bid> Bids { get; set; }
            
    }
}
