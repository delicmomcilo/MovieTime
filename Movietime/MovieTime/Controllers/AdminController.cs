using BLL;
using BLL.Interfaces;
using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    public class AdminController : SuperController
    {
        private IAdminBLL _adminBLL;
        private IKundeBLL _kundeBLL;
        private IFilmBLL _filmBLL;
        private ISjangerBLL _sjangerBLL;
        private IOrdreBLL _ordreBLL;
        private IEndringBLL _endringBLL;

        [ExcludeFromCodeCoverage]
        public AdminController()
        {
            _adminBLL = new AdminBLL();
            _kundeBLL = new KundeBLL();
            _filmBLL = new FilmBLL();
            _sjangerBLL = new SjangerBLL();
            _ordreBLL = new OrdreBLL();
            _endringBLL = new EndringBLL();
        }

        public AdminController(IAdminBLL adminStub, IKundeBLL kundeStub, IFilmBLL filmStub, ISjangerBLL sjangerStub, IOrdreBLL ordreStub, IEndringBLL endringStub)
        {
            _adminBLL = adminStub;
            _kundeBLL = kundeStub;
            _sjangerBLL = sjangerStub;
            _filmBLL = filmStub;
            _ordreBLL = ordreStub;
            _endringBLL = endringStub;

        }
        public ActionResult Dashboard()
        {

            if (SjekkAdmin())
            {
                if (TempData["Melding"] != null)
                {
                    ViewBag.Melding = TempData["Melding"].ToString();
                    TempData.Remove("Melding");
                }
                if (TempData["Error"] != null)
                {
                    ViewBag.Error = TempData["Error"].ToString();
                    TempData.Remove("Error");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }
        }


        public ActionResult Kunder()
        {
            if (SjekkAdmin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }
        }

        public ActionResult Filmer()
        {
            if (SjekkAdmin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }
        }

        public ActionResult Ordrer()
        {
            if (SjekkAdmin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }
        }
        public ActionResult LagKunde()
        {
            if (SjekkAdmin())
            {
                var registreringsmodel = new KundeRegistreringViewModel();
                return View(registreringsmodel);
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }

        }

        [HttpPost]
        public ActionResult LagKunde(KundeRegistreringViewModel kunde)
        {
            if (SjekkAdmin())
            {
                if (ModelState.IsValid)
                {
                    var Registrert = _kundeBLL.Registrer(kunde);
                    if (Registrert)
                    {
                        return RedirectToAction("Dashboard");
                    }
                }
                ViewBag.Error = "Noe gikk galt under oppretelsen av kunden";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");

            }

        }

        public ActionResult SlettKunde(int kID)
        {
            if (SjekkAdmin())
            {
               
               if(kID != (int)Session[KID])
               {
                    var slettet = _kundeBLL.SlettKunde(kID);
                    if (slettet)
                    {
                        TempData["Melding"] = "Kunde slettet";
                        return RedirectToAction("Dashboard");
                    }
                }

                TempData["Error"] = "Noe gikk galt under fjerning av kunden";
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Login", "Kunde");

            }

        }

        public ActionResult EndreKunde(int kID)
        {
            if (SjekkAdmin())
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
                var kunde = _kundeBLL.HentKunde(kID);
                if(kunde != null)
                {
                    return View(kunde);
                }
                TempData["Error"] = "Fant ikke kunden som skulle endres";
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Login", "Kunde");
        }

     

        [HttpPost]
        public ActionResult EndreKunde(KundeEndreViewModel kunde)
        {
            if (SjekkAdmin())
            {
                if (ModelState.IsValid)
                {
                    if (_kundeBLL.Endre(kunde, kunde.ID))
                    {
                        TempData["Melding"] = "Bruker Oppdatert";
                        return RedirectToAction("EndreKunde", new { kID = kunde.ID });
                    }
                }
                TempData["Error"] = "En feil oppstod under endring av kunde";
                return RedirectToAction("EndreKunde", new {kID = kunde.ID });
            
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult _KundeOrdreListe(int kID)
        {
            if (SjekkAdmin())
            {
                var ordre = _ordreBLL.HentAlleOrdre(kID);
                return PartialView(ordre);

            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult VisKundeOrdre(int OrdreID, int kID)
        {
            if (SjekkAdmin())
            {
                var ordre = _ordreBLL.HentOrdre(OrdreID, kID);
                return View(ordre);
            }
            return RedirectToAction("Login", "Kunde");
        }
        public ActionResult OpprettFilm()
        {
            if (SjekkAdmin()) {
                var film = new Film();
                return View(film);
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }
            
        }

        [HttpPost]
        public ActionResult OpprettFilm(Film nyFilm)
        {
            if (SjekkAdmin())
            {
                if (ModelState.IsValid)
                {
                    var opprettet = _filmBLL.OpprettFilm(nyFilm);
                    if (opprettet)
                    {
                        return RedirectToAction("Dashboard");
                    }
                }
                ViewBag.Error = "En feil oppstod under opprettelse av film";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Kunde");
            }


        }

        public ActionResult EndreFilm(int FilmID)
        {
            if (SjekkAdmin())
            {
                var Film = _filmBLL.HentFilm(FilmID);
                if (Film != null)
                {
                    return View(Film);
                }
                TempData["Error"] = "Fant ikke filmen som skulle endres";
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Login", "Kunde");
            
        }

        [HttpPost]
        public ActionResult EndreFilm(Film innFilm)
        {
            if (SjekkAdmin())
            {
                if (ModelState.IsValid)
                {
                    var endretFilm = _filmBLL.EndreFilm(innFilm);
                    if (endretFilm)
                    {
                        TempData["Melding"] = "Filmen er endret";
                        return RedirectToAction("Dashboard");
                    } 
                }
                
                ViewBag.Error = "En feil har oppstått under endring av denne filmen";
                return View(innFilm);
            }
            return RedirectToAction("Login", "Kunde");
            
        }


        
        public ActionResult SlettFilm(int FilmID)
        {
            if (SjekkAdmin())
            {
                var OK = _filmBLL.SlettFilm(FilmID);
                if (OK)
                {
                    TempData["Melding"] = "Film slettet";
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["Error"] = "En feil har oppstått under fjerning av filmen";
                    return RedirectToAction("Dashboard");
                }
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult SlettOrdre(int oID)
        {
            if (SjekkAdmin())
            {
                var OK = _ordreBLL.SlettOrdre(oID);
                if (OK)
                {
                    TempData["Melding"] = "Ordre slettet";
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["Error"] = "En feil har oppstått under fjerning av ordre";
                    return RedirectToAction("Dashboard");
                }
            }
            return RedirectToAction("Login", "Kunde");
        }


        public ActionResult OpprettSjanger()
        {
            if (SjekkAdmin())
            {
                var sjanger = new Sjanger();
                return View(sjanger);
            }
            return RedirectToAction("Login","Kunde");
            
        }

        [HttpPost]
        public ActionResult OpprettSjanger(Sjanger nySjanger)
        {
            if (SjekkAdmin())
            {
                if (ModelState.IsValid)
                {
                    var Opprettet = _sjangerBLL.LagSjanger(nySjanger);
                    if (Opprettet)
                    {
                        TempData["Melding"] = "Sjanger opprettet";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.Error = "Denne sjangeren eksisterer allerede";
                        return View();
                    }

                }
                ViewBag.Error = "Noe gikk galt under opprettelsen av sjangeren, vennligst prøv igjen.";
                return View();
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult TildelRettighet(int kID, int rettighet)
        {
            if (SjekkAdmin())
            {
                if((int)Session[KID] != kID)
                {
                    var tildelt = _kundeBLL.TildelRettigheter(kID, rettighet);
                    if (tildelt)
                    {
                        if (rettighet == 1)
                        {
                            TempData["Melding"] = "Endret til administrator";
                            return RedirectToAction("EndreKunde", new { kID = kID });
                        }
                        TempData["Melding"] = "Endret til kunde";
                        return RedirectToAction("EndreKunde", new { kID = kID });
                    }
                }
                TempData["Error"] = "Noe gikk galt under endring av rettighetene til denne brukeren";
                return RedirectToAction("EndreKunde", new { kID = kID });
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult _DashboardInfo()
        {
            if (SjekkAdmin())
            {
                var dashboard = _adminBLL.HentDashboardInfo();
                return PartialView(dashboard);
            }
            return RedirectToAction("Login", "Kunde");
        }
        public ActionResult _KundeListe(int MedRettighet)
        {
            if (SjekkAdmin())
            {
                var kunder = _kundeBLL.HentAlle(MedRettighet);
                return PartialView(kunder);

            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult _FilmListe()
        {
            if (SjekkAdmin())
            {
                var filmer = _filmBLL.HentAlle();
                return PartialView(filmer);
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult _OrdreListe()
        {
            if (SjekkAdmin())
            {
                var ordre = _ordreBLL.HentAlleOrdre();
                return PartialView(ordre);
            }
            return RedirectToAction("Login", "Kunde");
        }

        public ActionResult _EndringListe()
        {
            if (SjekkAdmin()) {
                var endringer = _endringBLL.Hent();
                return PartialView(endringer);
            }
            return RedirectToAction("Login", "Kunde");
        }

        private bool SjekkAdmin()
        {
            return Session[ADMIN] != null && (bool)Session[ADMIN] == true;
        }
    }
}