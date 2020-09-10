using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace BLL
{
    public class SjangerBLL : ISjangerBLL
    {
        private IDBSjanger _dbSjanger;
        private IDBFilm _dbFilm;

        [ExcludeFromCodeCoverage]
        public SjangerBLL()
        {
            _dbSjanger = new DBSjanger();
            _dbFilm = new DBFilm();

        }

        public SjangerBLL(IDBSjanger dbSjangerStub, IDBFilm dbFilmStub)
        {
            _dbSjanger = dbSjangerStub;
            _dbFilm = dbFilmStub;
        }

        public bool LagSjanger(Sjanger sjanger)
        {
            return _dbSjanger.LagSjanger(sjanger);

        }
        [ExcludeFromCodeCoverage]
        public List<Film> HentFilmerSjanger(int id)
        {
            return _dbFilm.HentFilmerSjanger(id);
        }


        [ExcludeFromCodeCoverage]
        public string HentAlleSjangreJson()
        {
            var sjangre = _dbSjanger.HentAlleSjangreJson();
            if(sjangre != null)
            {
                var jsonSerializer = new JavaScriptSerializer();
                var json = jsonSerializer.Serialize(sjangre);
                return json;
            }

            return "Fant ingen sjangre"; 
        }

        [ExcludeFromCodeCoverage]
        public string HentFilmerSjangerJson(int id)
        {
            var filmerSjanger = _dbFilm.HentFilmSjangerJson(id);
            if(filmerSjanger != null)
            {
                var jsonSerializer = new JavaScriptSerializer();
                var json = jsonSerializer.Serialize(filmerSjanger);
                return json;
            }

            return "Fant ingen sjangre på denne filmen";
        }
    }
}
