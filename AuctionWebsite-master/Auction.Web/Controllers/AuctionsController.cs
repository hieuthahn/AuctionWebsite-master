﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction.Entities;
using Auction.Services;
using Auction.Web.ViewsModels;

namespace Auction.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService auctionsService = new AuctionsService();
        CategoriesService categoriesService = new CategoriesService();
        SharedService sharedService = new SharedService();

        /*[HttpGet]*/
        public ActionResult Index(int? categoryID, string searchTerm, int? pageNo)
        {
            AuctionsListingViewModels model = new AuctionsListingViewModels();


            model.PageTitle = "Auctions";
            model.PageDescription = "Auctions Listing Page";

            model.CategoryID = categoryID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            model.Categories = categoriesService.GetAllCategories();

            return View(model);
        }


        public ActionResult Listing(int? categoryID, string searchTerm, int? pageNo)
        {
            var pageSize = 20;

            AuctionsListingViewModels model = new AuctionsListingViewModels();
            //model.Auctions = auctionsService.GetAllAuctions();
            model.Auctions = auctionsService.SearchAuctions(categoryID, searchTerm, pageNo, pageSize);

            var totalAuctions = auctionsService.GetAuctionCount(categoryID, searchTerm);

            model.Pager = new Pager(totalAuctions, pageNo, pageSize);

            return PartialView(model);

        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionViewModels model = new CreateAuctionViewModels();

            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(CreateAuctionViewModels model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                Auction.Entities.Auction auction = new Auction.Entities.Auction();
                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Description = model.Description;
                auction.ActualAmount = model.ActualAmount;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;

                //check if we have AuctionPictureIDs posted back from form
                if (!string.IsNullOrEmpty(model.AuctionPictures))
                {
                    //LINQ
                    var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ID => int.Parse(ID)).ToList();

                    auction.AuctionPictures = new List<AuctionPicture>();

                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
                }
                //foreach (var picID in pictureIDs)
                //{
                //    var auctionPicture = new AuctionPicture();
                //    auctionPicture.PictureID = picID;

                //    auction.AuctionPicture.Add(auctionPicture);
                //}

                auctionsService.SaveAuction(auction);

                result.Data = new { Suscess = true };
            }
            else
            {

                result.Data = new { Suscess = true, Error = "Unalble to save Auction. Please enter valid values." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CreateAuctionViewModels model = new CreateAuctionViewModels();
            var auction = auctionsService.GetAuctionByID(ID);
            model.ID = auction.ID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.ActualAmount = auction.ActualAmount;
            model.Description = auction.Description;
            model.StartingTime = auction.StartingTime;
            model.EndingTime = auction.EndingTime;

            model.Categories = categoriesService.GetAllCategories();
            model.AuctionPicturesList = auction.AuctionPictures;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateAuctionViewModels model)
        {
            Auction.Entities.Auction auction = new Auction.Entities.Auction();
            auction.ID = model.ID;
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;



            if (!string.IsNullOrEmpty(model.AuctionPictures))
            {
                //LINQ
                var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(ID => int.Parse(ID)).ToList();

                auction.AuctionPictures = new List<AuctionPicture>();

                auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { AuctionID = auction.ID, PictureID = x }).ToList());
            }

            auctionsService.UpdateAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction.Entities.Auction auction)
        {
            auctionsService.DeleteAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            Auction.Entities.Auction auction = new Auction.Entities.Auction();
            AuctionsDetailsViewModels model = new AuctionsDetailsViewModels();
            model.EntityID = (int)EntityEnums.Auction;
            model.Auction = auctionsService.GetAuctionByID(ID);

            model.BidsAmount = model.Auction.ActualAmount + model.Auction.Bids.Sum(x => x.BidAmount);

            var latestBidder = model.Auction.Bids.OrderByDescending(x => x.Timestamp).FirstOrDefault();

            model.LatesBidder = latestBidder != null ? latestBidder.User : null;

            model.Comments = sharedService.GetComments((int)EntityEnums.Auction, model.Auction.ID);

            model.PageTitle = "Auctions Details: " + model.Auction.Title;
            /*model.PageDescription = model.Auction.Description.Substring(0, 10);*/

            /*auction.ActualAmount = model.BidsAmount;
            auctionsService.UpdateBidAuction(auction);*/
            return View(model);
        }
    }
}