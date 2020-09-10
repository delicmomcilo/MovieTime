using AutoMapper;
using BLL;
using Model.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    [ExcludeFromCodeCoverage]
    public class KundeController : SuperController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(KundeLoginViewModel innKunde)
        {
            if (ModelState.IsValid)
            {
                var kundeBLL = new KundeBLL();
                var kundeLoggetInn = kundeBLL.Login(innKunde);

                if (kundeLoggetInn)
                {
                    var kunde = kundeBLL.Finn(innKunde.Epost);
                    
                    Session[LOGGET_INN] = true;
                    Session[KID] = kunde.ID;
                    Session[NAVN] = kunde.Fornavn;
                    if (kunde.ErAdmin == true)
                    {
                        Session[ADMIN] = true;
                        return RedirectToAction("Dashboard", "Admin", new { area = "" });

                    }

                    return RedirectToAction("Filmer", "Film", new { area = "" });
                }
                
                    ViewBag.Error = "Ugyldig E-post eller passord.";
                    return View();
            }
            else
            {
                ViewBag.Error = "Ugyldig E-post eller passord.";
                return View();
            }
           
        }
        public ActionResult Loggut()
        {
            Session.Abandon();
            return RedirectToAction("Hjem", "Home", new { area = "" });
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrer(KundeRegistreringViewModel innKunde)
        {
            if (ModelState.IsValid)
            {
                var kundeBLL = new KundeBLL();
                bool OK = kundeBLL.Registrer(innKunde);
                if (OK)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.Error = "Denne Eposten eksisterer allerede";
                
            }

            return View();

        }

        public ActionResult Profil()
        {
         
          if (Session[LOGGET_INN] != null)
          {
              if(TempData["Error"] != null)
              {
                    ViewBag.Error = TempData["Error"].ToString();
                    TempData.Remove("Error");
              }
              if (TempData["Melding"] != null)
              {
                    ViewBag.Melding = TempData["Melding"].ToString();
                    TempData.Remove("Melding");
              }
              var kundeBLL = new KundeBLL();
              var kID = (int)Session[KID];
              var funnetKunde = kundeBLL.FinnKundeID(kID);

              if(funnetKunde != null)
              {
                return View(Mapper.Map<KundeEndreViewModel>(funnetKunde));

              }
            }        
            
            return RedirectToAction("Login", "Kunde", new { area = "" });

        }

        [HttpPost]
        public ActionResult Profil(KundeEndreViewModel endretKunde)
        {

            if (Session[LOGGET_INN] != null)
            {
                if (ModelState.IsValid)
                {
                    var kundeBLL = new KundeBLL();
                    var kID = (int)Session[KID];
                    var endret = kundeBLL.Endre(endretKunde, kID);
                    if (endret)
                    {
                        Session[NAVN] = endretKunde.Fornavn;
                        TempData["Melding"] = "Profil endret!";
                        return RedirectToAction("Profil");
                    }
                    else
                    {
                        TempData["Error"] = "Noe gikk galt, vennligst prøv å endre profilen på nytt.";
                        return RedirectToAction("Profil");
                    }
                }
                else
                {
                    return View();
                }    
            }
            else
            {
                return RedirectToAction("Login", "Kunde", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult EndrePassord(PassordViewModel endretPassord)
        {
            if(Session[LOGGET_INN] != null)
            {
                if (ModelState.IsValid)
                {
                    var kundeBLL = new KundeBLL();
                    var kID = (int)Session[KID];
                    var endret = kundeBLL.EndrePassord(endretPassord, kID);
                    if (endret)
                    {
                        TempData["Melding"] = "Passord er endret!";
                        return RedirectToAction("Profil");
                    }
                }
            }
            TempData["Error"] = "Noe gikk galt, vennligst prøv å endre passordet på nytt.";
            return RedirectToAction("Profil");
        }
    }
}