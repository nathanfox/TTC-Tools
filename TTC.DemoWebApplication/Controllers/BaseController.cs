using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTC.Tools;

namespace TTC.DemoWebApplication.Controllers
{
   public class BaseController : Controller
   {
      ITypeInstanceDictionary _typeViewData = new TypeInstanceDictionary();

      public ITypeInstanceDictionary TypeViewData
      {
         get
         {
            return _typeViewData;
         }
         set
         {
            _typeViewData = value;
         }
      }
   }
}
