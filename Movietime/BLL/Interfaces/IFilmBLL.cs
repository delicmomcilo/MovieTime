using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface IFilmBLL
    {
        bool EndreFilm(Film innFilm);
        List<Film> HentAlle();
        string HentAlleJson();
        Film HentFilm(int id);
        bool OpprettFilm(Film nyFilm);
        bool SlettFilm(int id);
    }
}