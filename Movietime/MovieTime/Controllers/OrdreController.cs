using AutoMapper;
using BLL;
using Model;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    [ExcludeFromCodeCoverage]
    public class OrdreController : SuperController
    {

        public HandleKurv HentHandlekurv()
        {

            var ordreBLL = new OrdreBLL();
            if(Session[LOGGET_INN] != null)
            {
                if (Session[HANDLEKURV] == null)
                {
                    Session[HANDLEKURV] = ordreBLL.LagHandleKurv((int)Session[KID]);
                    return (HandleKurv)Session[HANDLEKURV];
                }
                return (HandleKurv)Session[HANDLEKURV];
            }
            else
            {
                return null;
            }
           
        }

        public ActionResult Bestillinger()
        {
            if (Session[LOGGET_INN] != null)
            {
                var kID = (int)Session[KID];
                var ordreBLL = new OrdreBLL();
                var ordre = ordreBLL.HentAlleOrdre(kID);
                return View(ordre);
            }
            else
            {
                return RedirectToAction("Login", "Kunde", new { area = "" });
            }
        }

        public ActionResult VisOrdre(int ordreID)
        {

            if (Session[LOGGET_INN] != null)
            {
                var kID = (int)Session[KID];
                var ordreBLL = new OrdreBLL();
                var ordre = ordreBLL.HentOrdre(ordreID,kID);
                return View(ordre);
            }
            else
            {
                return RedirectToAction("Login", "Kunde", new { area = "" });

            }
        }

        public ActionResult _HandleKurv()
        {
            if(Session[HANDLEKURV] == null)
            {
                HentHandlekurv();
                return PartialView();
            }
            else
            {
                var kurv = (HandleKurv)Session[HANDLEKURV];
                return PartialView(kurv);
            }
        }

        public ActionResult LagOrdre(int id)
        {
         
            if(Session[LOGGET_INN] != null)
            {
                var kID = (int)Session[KID];
                var ordreBLL = new OrdreBLL();
              
                var kurv = HentHandlekurv();
                ordreBLL.LeggTilHandleKurv(kurv, id);
                return RedirectToAction("_HandleKurv");

            }
            else
            {
                return Json(new { fallBack = Url.Action("Login", "Kunde") });
               

            }
        }

        public ActionResult Kassen()
        {
            if(Session[LOGGET_INN] != null)
            {
                if (TempData["Error"] != null)
                {
                    ViewBag.Error = TempData["Error"].ToString();
                    TempData.Remove("Error");
                }
                var kurv = HentHandlekurv();
                return View(Mapper.Map<HandleKurv>(kurv));
            }
            else
            {
                return RedirectToAction("Login", "Kunde", new { area = "" });

            }  
        }

        [HttpPost]
        public ActionResult Betal()
        {
            if (Session[LOGGET_INN] != null)
            {
                HandleKurv kurv = HentHandlekurv();
                if (kurv != null && kurv.Antall > 0)
                {
                    var kID = (int)Session[KID];
                    var ordreBLL = new OrdreBLL();
                    var OK = ordreBLL.OpprettOrdre(kurv, kID);
                    if (OK)
                    {
                        Session[HANDLEKURV] = null;
                        return RedirectToAction("Bestillinger");
                    }
                    TempData["Error"] = "Du har ingenting i handlekurven.";
                    return RedirectToAction("Kassen");
                }
                else
                {
                    TempData["Error"] = "Du har ingenting i handlekurven.";
                    return RedirectToAction("Kassen");
                }   
            }
            else
            {
                return RedirectToAction("Login", "Kunde", new { area = "" });
            }
        }

        public ActionResult FjernFilm(int filmID)
        {
            if(Session[LOGGET_INN] != null)
            {
                var kurv = HentHandlekurv();
                var ordreBLL = new OrdreBLL();
                ordreBLL.FjernFraHandleKurv(kurv, filmID);
                return RedirectToAction("Kassen");
               
            }
            return RedirectToAction("Kassen");
        }

        [HttpGet]
        public ActionResult HentHandleKurv()
        {
            var kurv = HentHandlekurv();
            return PartialView("_HandleKurv", kurv);
        }
    }
}