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
         HomeViewData viewData = new HomeViewData();

         viewData.Message = "Welcome to ASP.NET MVC!";
         viewData.Message2 = "And welcome again!";
         viewData.Message3 = "And welcome yet again!";

         TypedViewData.Add<HomeViewData>(viewData);

         return View(TypedViewData);
      }

      public ActionResult About()
      {
         return View();
      }
   }
}
