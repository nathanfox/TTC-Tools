using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.DemoWebApplication.ViewData
{

   public interface IIndexViewData
   {
      string Message { get; set; }
      string Message2 { get; set; }
      string Message3 { get; set; }
   }

   public class IndexViewData : IIndexViewData
   {
      public string Message { get; set; }
      public string Message2 { get; set; }
      public string Message3 { get; set; }
   }
}
