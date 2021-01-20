using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineVoting.Models;

namespace onlineVoting.Base
{
    public class BaseController : Controller
    {
        WP_VoteIT_DBEntities1 db = new WP_VoteIT_DBEntities1();
        //public string xUsername { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/StartingLogin");
            }
            else
            {
                //xUsername = Session["UserName"].ToString();
                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}