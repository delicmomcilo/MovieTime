using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBFilm
    {
        bool EndreFilm(Film innFilm);
        List<Film> HentAlle();
        Film HentFilm(int id);
        List<Film> HentFilmerSjanger(int id);
        List<JsonFilm> HentFilmSjangerJson(int id);
        bool OpprettFilm(Film nyFilm);
        bool SlettFilm(int id);
    }
}