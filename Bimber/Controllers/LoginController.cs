using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Diagnostics;
using System.Web.Services;

namespace Bimber.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        [WebMethod(EnableSession=true)]
        public ActionResult Index(string dataString)
        {

            FacebookClient fbClient = new FacebookClient();

            fbClient.AccessToken = dataString;
            fbClient.
            if(dataString != null)
            {
                Debug.WriteLine("dlugosc jsona = " + dataString.Length);
                Debug.WriteLine("value jsona = " + dataString);
            }
            else
            {
                Debug.WriteLine("dlugosc jsona = 0");
            }
           

            //var accessToken = context.Request["accessToken"];
            //context.Session["AccessToken"] = accessToken;

            //context.Response.Redirect("/MyUrlHere");

            return View();
        }
    }
}