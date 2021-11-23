using Auction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.ViewsModels
{
    public class CategoriesListingViewModels : PageViewModels
    {
        public List<Auction.Entities.Category> Categories { get; set; }
    }
    public class CategoriesViewModels : PageViewModels
    {
        public List<Auction.Entities.Category> AllCategories { get; set; }
    }

    public class CategoryDetailsViewModels : PageViewModels
    {
        public Auction.Entities.Category Category { get; set; }
    }

    public class CategoryViewModels : PageViewModels
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Auction.Entities.Auction> Auctions { get; set; }
    }
}