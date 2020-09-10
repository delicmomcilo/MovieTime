using System;
using BLL;
using DAL.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webapplikasjoner_Del_1_2018.Controllers;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Model.ViewModel;
using Model;

namespace Enhetstest.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {

        private static AdminController AdminControllerSession(bool ErAdmin)
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new DBOrdreStub(), new DBKundeStub(), new DBFilmStub()), new KundeBLL(new DBKundeStub()), new FilmBLL(new DBFilmStub()), new SjangerBLL(new DBSjangerStub(), new DBFilmStub()), new OrdreBLL(new DBOrdreStub()), new EndringBLL(new DBEndringStub()));
            SessionMock.InitializeController(controller);
            if(ErAdmin == true)
            {
                controller.Session["Admin"] = true;
                controller.Session["kID"] = 1;
            }
            else
            {
                controller.Session["Admin"] = false;
            }

            return controller;

        }

        [TestMethod]
        public void DashboardTestErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.Dashboard();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void DashboardTestErIkkeAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.Dashboard();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisOrdrer_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.Ordrer();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void VisOrdrer_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.Ordrer();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }
        [TestMethod]
        public void VisKunder_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.Kunder();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void VisKunder_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.Kunder();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisLagKundeErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.LagKunde();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void VisLagKundeErIkkeAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.LagKunde();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void LagKunde_riktig_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeRegistreringViewModel()
            {
                Fornavn = "Henry",
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 9",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik",
                Passord = "Henrytran1"
            };

            //Act
            var resultView = (RedirectToRouteResult)controller.LagKunde(kunde);

            //Assert
            Assert.AreEqual("Dashboard", resultView.RouteValues["action"]);
        }

        [TestMethod]
        public void LagKunde_ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            var kunde = new KundeRegistreringViewModel()
            {
                Fornavn = "Henry",
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 9",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik",
                Passord = "Henrytran1"
            };

            //Act
            var resultView = (RedirectToRouteResult)controller.LagKunde(kunde);

            //Assert
            Assert.AreEqual("Login", resultView.RouteValues["action"]);
        }
        [TestMethod]
        public void LagKunde_feil_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeRegistreringViewModel()
            {
                Fornavn = "Henry"
            };

            //Act
            var resultView = (ViewResult)controller.LagKunde(kunde);

            //Assert
            Assert.AreEqual("", resultView.ViewName);
        }

        [TestMethod]
        public void LagKunde_feil_validering_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeRegistreringViewModel(){};
            controller.ViewData.ModelState.AddModelError("Fornavn","Ikke oppgitt fornavn");
            //Act
            var resultView = (ViewResult)controller.LagKunde(kunde);

            //Assert
            Assert.IsTrue(resultView.ViewData.ModelState.Count == 1);
            Assert.AreEqual("", resultView.ViewName);
        }

        [TestMethod]
        public void EndreKunde_riktig_admin_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeEndreViewModel()
            {
                ID = 1,
                Fornavn = "Henry",
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 29",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik"
            };

            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(kunde);

            //Assert
            Assert.AreEqual("Bruker Oppdatert",controller.TempData["Melding"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EndreKunde_feil_admin_Post()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            var kunde = new KundeEndreViewModel()
            {
                ID = 1,
                Fornavn = "Henry",
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 9",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik"
            };

            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(kunde);

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EndreKunde_feil_validering_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeEndreViewModel()
            {
                ID = 1,
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 9",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik"
            };

            controller.ViewData.ModelState.AddModelError("Fornavn", "Ikke oppgitt fornavn");

            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(kunde);

            //Assert

            Assert.AreEqual("En feil oppstod under endring av kunde", controller.TempData["Error"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EndreKunde_feil_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var kunde = new KundeEndreViewModel()
            {
                ID = 0,
                Etternavn = "Tran",
                Fodselsdag = new DateTime(1997, 12, 25),
                Adresse = "Brageveien 9",
                Mobilnummer = 41379773,
                Epost = "Henry.nor@gmail.com",
                Postnummer = "3274",
                Poststed = "Larvik"
            };

            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(kunde);

            //Assert

            Assert.AreEqual("En feil oppstod under endring av kunde", controller.TempData["Error"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettKunde_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettKunde(2);

            //Assert
            Assert.AreEqual("Kunde slettet", controller.TempData["Melding"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettKunde_feil_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettKunde(20);

            //Assert
            Assert.AreEqual("Noe gikk galt under fjerning av kunden", controller.TempData["Error"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettKunde_feil_ID_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettKunde(-1);

            //Assert
            Assert.AreEqual("Noe gikk galt under fjerning av kunden", controller.TempData["Error"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
        }


        [TestMethod]
        public void SlettKunde_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            //Act
            var result = (RedirectToRouteResult)controller.SlettKunde(2);

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettOrdre_Riktig_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettOrdre(1);

            //Assert
            Assert.AreEqual("Ordre slettet", controller.TempData["Melding"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettOrdre_Feil_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettOrdre(50);

            //Assert
            Assert.AreEqual("En feil har oppstått under fjerning av ordre", controller.TempData["Error"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SlettOrdre_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            //Act
            var result = (RedirectToRouteResult)controller.SlettOrdre(1);

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisFilmer_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.Filmer();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void VisFilmer_ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.Filmer();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisOpprettFilm_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.OpprettFilm();

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void VisOpprettFilmErIkkeAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.OpprettFilm();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void OpprettFilm_riktig_Admin_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var film = new Film()
            {
                ID = 4,
                Filmnavn = "Deathwish",
                Filmbilde = "/Content/Images/Deathwish.jpg",
                Beskrivelse ="Beskrivelse",
                Fremhevet = false,
                Pris = 100,
                Sjanger = "Action"

            };

            //Act
            var resultView = (RedirectToRouteResult)controller.OpprettFilm(film);

            //Assert
            Assert.AreEqual("Dashboard", resultView.RouteValues["action"]);
        }

        [TestMethod]
        public void OpprettFilm_ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            var film = new Film()
            {
                ID = 4,
                Filmnavn = "Deathwish",
                Filmbilde = "/Content/Images/Deathwish.jpg",
                Beskrivelse = "Beskrivelse",
                Fremhevet = false,
                Pris = 100,
                Sjanger = "Action"

            };

            //Act
            var resultView = (RedirectToRouteResult)controller.OpprettFilm(film);

            //Assert
            Assert.AreEqual("Login", resultView.RouteValues["action"]);
        }

        [TestMethod]
        public void OpprettFilm_feil_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var film = new Film()
            {
                ID = 4,
                Filmnavn = "Deathwish"
            };

            //Act
            var resultView = (ViewResult)controller.OpprettFilm(film);
      
            //Assert
            Assert.AreEqual("", resultView.ViewName);
        }

        [TestMethod]
        public void OpprettFilm_feil_Validering_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            controller.ViewData.ModelState.AddModelError("Pris", "Ikke oppgitt pris");


            var film = new Film()
            {
                ID = 4,
                Filmnavn = "Deathwish"
            };

            //Act
            var resultView = (ViewResult)controller.OpprettFilm(film);

            //Assert
            Assert.AreEqual("", resultView.ViewName);
        }

        [TestMethod]
        public void VisEndreFilm_Riktig_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.EndreFilm(2);
            var resultFilm = (Film)result.Model;
            //Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual("Deadpool", resultFilm.Filmnavn);
        }

        [TestMethod]
        public void VisEndreFilm_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.EndreFilm(2);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
    
        }

        [TestMethod]
        public void VisEndreFilm_Feil_ID()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (RedirectToRouteResult)controller.EndreFilm(-1);
            //Assert
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);

        }

        [TestMethod]
        public void EndreFilm_Riktig_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var film = new Film()
            {
                ID = 1,
                Filmnavn = "Deadpool",
                Filmbilde = "/Content/Images/Deadpool.jpg",
                Beskrivelse = "Deadpool er en Action film",
                Pris = 49,
                Fremhevet = true,
                FremhevetBilde = "/Content/Images/DeadpoolBanner.jpg",
                Sjanger = "Komedie"
            };

            //Act
            var result = (RedirectToRouteResult)controller.EndreFilm(film);
            //Assert
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);

        }

        [TestMethod]
        public void EndreFilm_Feil_Admin_Post()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            var film = new Film()
            {
                ID = 1,
                Filmnavn = "Deadpool",
                Filmbilde = "/Content/Images/Deadpool.jpg",
                Beskrivelse = "Deadpool er en Action film",
                Pris = 49,
                Fremhevet = true,
                FremhevetBilde = "/Content/Images/DeadpoolBanner.jpg",
                Sjanger = "Komedie"
            };

            //Act
            var result = (RedirectToRouteResult)controller.EndreFilm(film);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);

        }
        [TestMethod]
        public void EndreFilm_Feil_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            var film = new Film()
            {
                ID = 1,
                Filmnavn = "Deadpool",
                Filmbilde = "/Content/Images/Deadpool.jpg",
                Beskrivelse = "Deadpool er en Action film",
                Pris = 49,
                Fremhevet = true,
                FremhevetBilde = "/Content/Images/DeadpoolBanner.jpg"
            };

            //Act
            var result = (ViewResult)controller.EndreFilm(film);
            //Assert
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void EndreFilm_Feil_Validering_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            controller.ViewData.ModelState.AddModelError("Sjanger", "Ikke oppgitt sjanger");

            var film = new Film()
            {
                ID = 1,
                Filmnavn = "Deadpool",
                Filmbilde = "/Content/Images/Deadpool.jpg",
                Beskrivelse = "Deadpool er en Action film",
                Pris = 49,
                Fremhevet = true,
                FremhevetBilde = "/Content/Images/DeadpoolBanner.jpg"
            };

            //Act
            var result = (ViewResult)controller.EndreFilm(film);
            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void SlettFilm_Riktig_ID()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettFilm(1);
            //Assert
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
            Assert.AreEqual("Film slettet", controller.TempData["Melding"]);

        }

        [TestMethod]
        public void SlettFilm_Feil_ID()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.SlettFilm(-1);
            //Assert
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);
            Assert.AreEqual("En feil har oppstått under fjerning av filmen", controller.TempData["Error"]);

        }
        [TestMethod]
        public void SlettFilm_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            //Act
            var result = (RedirectToRouteResult)controller.SlettFilm(1);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisOpprettSjanger_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (ViewResult)controller.OpprettSjanger();
            //Assert
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void VisOpprettSjanger_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            //Act
            var result = (RedirectToRouteResult)controller.OpprettSjanger();
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);

        }
        [TestMethod]
        public void OpprettSjanger_Ikke_Admin_Post()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            var sjanger = new Sjanger();
            sjanger.sjanger = "Komedie";
            //Act
            var result = (RedirectToRouteResult)controller.OpprettSjanger(sjanger);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void OpprettSjanger_Riktig_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            var sjanger = new Sjanger();
            sjanger.sjanger = "Komedie";
            //Act
            var result = (RedirectToRouteResult)controller.OpprettSjanger(sjanger);
            //Assert
            Assert.AreEqual("Sjanger opprettet", controller.TempData["Melding"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);

        }

        [TestMethod]
        public void OpprettSjanger_Feil_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            var sjanger = new Sjanger();
           
            //Act
            var result = (ViewResult)controller.OpprettSjanger(sjanger);
            //Assert           
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void OpprettSjanger_Feil_Validering_Post()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            var sjanger = new Sjanger();
            controller.ViewData.ModelState.AddModelError("Sjanger", "Ikke oppgitt sjanger");


            //Act
            var result = (ViewResult)controller.OpprettSjanger(sjanger);
            //Assert           
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void VisDashboardInfo_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._DashboardInfo();
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisDashboardInfo_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._DashboardInfo();
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisEndringListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._EndringListe();
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisOrdreListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._OrdreListe();
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisOrdreListe_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._OrdreListe();
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisFilmListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._FilmListe();
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisKundeListe_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._KundeListe(0);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisKundeListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._KundeListe(0);
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisAdminListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._KundeListe(1);
            //Assert
            Assert.AreNotEqual(null, result.Model);

        }

        [TestMethod]
        public void VisFilmListe_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._FilmListe();
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisEndreKunde_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (ViewResult)controller.EndreKunde(2);
            //Assert
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void VisEndreKunde_feil_ID_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(0);
            //Assert
            Assert.AreEqual("Fant ikke kunden som skulle endres", controller.TempData["Error"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);

        }

        [TestMethod]
        public void VisEndreKunde_feil_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);
            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(20);
            //Assert
            Assert.AreEqual("Fant ikke kunden som skulle endres", controller.TempData["Error"]);
            Assert.AreEqual("Dashboard", result.RouteValues["action"]);

        }

        [TestMethod]
        public void VisEndreKunde_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);
            //Act
            var result = (RedirectToRouteResult)controller.EndreKunde(2);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);

        }

        [TestMethod]
        public void VisEndringListe_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._EndringListe();
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisKundeOrdreListe_Er_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (PartialViewResult)controller._KundeOrdreListe(1);
            //Assert
            Assert.AreNotEqual(null, result.Model);
        }

        [TestMethod]
        public void VisKundeOrdreListe_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller._KundeOrdreListe(1);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void VisKundeOrdre_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.VisKundeOrdre(1, 2);
            //Assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void VisKundeOrdre_Feil_ID_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (ViewResult)controller.VisKundeOrdre(1, 50);
            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void VisKundeOrdre_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.VisKundeOrdre(1, 2);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TildelRettighet_Admin_Riktig_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (RedirectToRouteResult)controller.TildelRettighet(2, 1);
            //Assert
            Assert.AreEqual("Endret til administrator", controller.TempData["Melding"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TildelRettighet_Kunde_Riktig_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (RedirectToRouteResult)controller.TildelRettighet(2, 0);
            //Assert
            Assert.AreEqual("Endret til kunde", controller.TempData["Melding"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TildelRettighet_feil_ErAdmin()
        {
            //Arrange
            var controller = AdminControllerSession(true);

            //Act
            var result = (RedirectToRouteResult)controller.TildelRettighet(20, 0);
            //Assert
            Assert.AreEqual("Noe gikk galt under endring av rettighetene til denne brukeren", controller.TempData["Error"]);
            Assert.AreEqual("EndreKunde", result.RouteValues["action"]);
        }


        [TestMethod]
        public void TildelRettighet_Ikke_Admin()
        {
            //Arrange
            var controller = AdminControllerSession(false);

            //Act
            var result = (RedirectToRouteResult)controller.TildelRettighet(1, 1);
            //Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }


    }
}
