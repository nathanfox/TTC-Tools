using System;
using System.Web.Mvc;

namespace TTC.Tools.Mvc
{
   /// <summary>
   /// Redirects using a 301 permanent redirect HTTP status code.
   /// </summary>
   public class PermanentRedirectResult : RedirectResult
   {
      public PermanentRedirectResult(string url) : base(url)
      {
      }

      public override void ExecuteResult(ControllerContext context)
      {
         base.ExecuteResult(context);
         context.HttpContext.Response.StatusCode = 301;
      }
   }
}
