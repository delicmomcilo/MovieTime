using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    [ExcludeFromCodeCoverage]
    public class SuperController : Controller
    {
        public const string KID = "kID";
        public const string LOGGET_INN = "Logget_inn";
        public const string NAVN = "Navn";
        public const string HANDLEKURV = "Handlekurv";
        public const string ADMIN = "Admin";

        public ActionResult Index()
        {
            return RedirectToAction("Hjem","Home", new { area = "" });
        }
       
    }
}