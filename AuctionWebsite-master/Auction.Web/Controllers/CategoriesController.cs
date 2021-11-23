using Auction.Services;
using Auction.Web.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            CategoriesListingViewModels model = new CategoriesListingViewModels();

            model.PageTitle = "Categories";
            model.PageDescription = "Categories Listing Page";
            return View(model);
        }


        public ActionResult Listing()
        {
            CategoriesListingViewModels model = new CategoriesListingViewModels();

            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);

        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoriesViewModels model = new CategoriesViewModels();


            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModels model)
        {
            Auction.Entities.Category category = new Auction.Entities.Category();
            category.Name = model.Name;
            category.Description = model.Description;

            categoriesService.SaveCategory(category);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CategoryViewModels model = new CategoryViewModels();
            var category = categoriesService.GetCategoryByID(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModels model)
        {
            Auction.Entities.Category category = new Auction.Entities.Category();
            category.ID = model.ID;
            category.Name = model.Name;
            category.Description = model.Description;

            categoriesService.UpdateCategory(category);

            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction.Entities.Category category)
        {
            categoriesService.DeleteCategory(category);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            CategoryDetailsViewModels model = new CategoryDetailsViewModels();
            model.Category = categoriesService.GetCategoryByID(ID);

            model.PageTitle = "Auctions Details: " + model.Category.Name;
            /*model.PageDescription = model.Category.Description.Substring(0, 10);*/

            return View(model);
        }

    }
}