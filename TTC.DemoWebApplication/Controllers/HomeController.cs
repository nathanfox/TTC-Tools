using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTC.DemoWebApplication.Controllers
{
   using ViewData;

   [HandleError]
   public class HomeController : BaseController
   {
      public ActionResult Index()
      {
         //ViewData["Message"] = "Welcome to ASP.NET MVC!";

         HomeViewData viewData = new HomeViewData();

         viewData.Message = "Welcome to ASP.NET MVC!";

         TypedViewData.Add<HomeViewData>(viewData);

         return View(TypedViewData);
      }

      public ActionResult About()
      {
         return View();
      }
   }
}
