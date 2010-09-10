using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTC.Tools.Mvc;

namespace TTC.DemoWebApplication.Controllers
{
   using ViewData;

   [HandleError]
   public class HomeController : BaseController
   {
      public ActionResult Index()
      {
         IIndexViewData viewData = new IndexViewData();

         viewData.Message = "Welcome to ASP.NET MVC!";
         viewData.Message2 = "And welcome again!";
         viewData.Message3 = "And welcome yet again!";

         TypeViewData.Add<IIndexViewData>(viewData);

         return View(TypeViewData);
      }

      public ActionResult About()
      {
         return View(TypeViewData);
      }

      public ActionResult DoPermanentRedirect()
      {
         return new PermanentRedirectResult("/home/index");
      }
   }
}
