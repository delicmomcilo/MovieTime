using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using BLL;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : SuperController
    {
      
        // GET: Home
        public ActionResult Hjem()
        {
            var filmBLL = new FilmBLL();
            var alleFilmer = filmBLL.HentAlle();
            
            return View(alleFilmer);
        }

    }
}