using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public string UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }

    }
}
