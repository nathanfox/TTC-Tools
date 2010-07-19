using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.DemoWebApplication.ViewData
{
   public interface IMasterPageViewData
   {
      string BodyId { get; set; }
   }


   public class MasterPageViewData : IMasterPageViewData
   {
      string IMasterPageViewData.BodyId { get; set; }
   }
}
