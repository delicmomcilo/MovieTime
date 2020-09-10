using BLL;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Webapplikasjoner_Del_1_2018.Controllers
{
    [ExcludeFromCodeCoverage]
    public class FilmController : SuperController
    {
        
        public ActionResult Filmer()
        {
            var filmBLL = new FilmBLL();
            var alleFilmer = filmBLL.HentAlle();
            return View(alleFilmer);   
        }

        public string HentAlleFilmerJson()
        {
            var filmBLL = new FilmBLL();
            return filmBLL.HentAlleJson();
        }
        public string HentAlleSjangreJson()
        {
            var sjangerBLL = new SjangerBLL();
            return sjangerBLL.HentAlleSjangreJson();
        }

        
        public string HentFilmerSjangerJson(int id)
        {
            var sjangerBLL = new SjangerBLL();
            return sjangerBLL.HentFilmerSjangerJson(id);
        }
            
    }
}