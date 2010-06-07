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
       ITypeInstanceDictionary _typedViewData = new TypeInstanceDictionary();
       
       public ITypeInstanceDictionary TypedViewData
       {
          get
          {
             return _typedViewData;
          }
          set
          {
             _typedViewData = value;
          }
       }
    }
}
