using Auction.Services;
using Auction.Web.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class HomeController : Controller
    {
        AuctionsService auctionsService = new AuctionsService();
        CategoriesService categoriesService = new CategoriesService();

        public ActionResult Index(int? categoryID, string searchTerm, int? pageNo)
        {
            /*AuctionsViewModels vModel = new AuctionsViewModels();

            vModel.PageTitle = "Đấu giá trực tuyến";
            vModel.PageDescription = "This is HomePage";

            var auctions = auctionsService.GetAllAuctions();

            vModel.AllAuctions = new List<Entities.Auction>();
            vModel.AllAuctions.AddRange(auctions);

            vModel.PromotedAuctions = auctionsService.GetPromotedAuctions();
           

            return View(vModel);*/
            AuctionsListingViewModels model = new AuctionsListingViewModels();

            model.CategoryID = categoryID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            

            model.Categories = categoriesService.GetAllCategories();
            return View(model);

        }

        public ActionResult Listing(int? categoryID, string searchTerm, int? pageNo)
        {
            var pageSize = 30;

            AuctionsListingViewModels model = new AuctionsListingViewModels();
            //model.Auctions = auctionsService.GetAllAuctions();
            model.Auctions = auctionsService.SearchAuctions(categoryID, searchTerm, pageNo, pageSize);

            var totalAuctions = auctionsService.GetAuctionCount(categoryID, searchTerm);

            model.Pager = new Pager(totalAuctions, pageNo, pageSize);


            return PartialView(model);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}