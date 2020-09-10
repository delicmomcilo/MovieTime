using BLL.Interfaces;
using DAL;
using Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;

namespace BLL
{
    public class FilmBLL : IFilmBLL
    {
        private IDBFilm _dbFilm;
        [ExcludeFromCodeCoverage]
        public FilmBLL() 
        {
            _dbFilm = new DBFilm();
        }

        public FilmBLL(IDBFilm dbFilmStub)
        {
            _dbFilm = dbFilmStub;
        }

        public bool OpprettFilm(Film nyFilm)
        {
            return _dbFilm.OpprettFilm(nyFilm);
        }

     

        public bool EndreFilm(Film innFilm)
        {
            return _dbFilm.EndreFilm(innFilm);
        }


        public List<Film> HentAlle()
        {
            return _dbFilm.HentAlle();
        }

        public Film HentFilm(int id)
        {
            return _dbFilm.HentFilm(id);
        }

        public bool SlettFilm(int id)
        {
            return _dbFilm.SlettFilm(id);
        }
        [ExcludeFromCodeCoverage]
        public string HentAlleJson()
        {
            var jsonFilmer = _dbFilm.HentAlle();
            

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(jsonFilmer);

            return json;
            
        }
        
     
    }
}
