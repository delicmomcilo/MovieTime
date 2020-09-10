using BLL.Interfaces;
using DAL;
using Model.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace BLL
{
    public class AdminBLL : IAdminBLL
    {
        private IDBOrdre _dbOrdre;
        private IDBKunde _dbKunde;
        private IDBFilm _dbFilm;
        [ExcludeFromCodeCoverage]
        public AdminBLL()
        {
            _dbOrdre = new DBOrdre();
            _dbKunde = new DBKunde();
            _dbFilm = new DBFilm();
        }

        public AdminBLL(IDBOrdre dbOrdreStub, IDBKunde dbKundeStub, IDBFilm dbFilmStub)
        {
            _dbOrdre = dbOrdreStub;
            _dbKunde = dbKundeStub;
            _dbFilm = dbFilmStub;
        }

        public DashboardInfoViewModel HentDashboardInfo()
        {
            var dashBoard = new DashboardInfoViewModel
            {
                TotaltSalg = _dbOrdre.HentTotaltSalg(),
                AntallKunder = _dbKunde.HentAlle(0).Count(),
                AntallFilmer = _dbFilm.HentAlle().Count()
            };
            return dashBoard;

        }


    }
}
