using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model;

namespace DAL.Stubs
{
    public class DBFilmStub : IDBFilm
    {
        private List<Film> filmer = new List<Film>()
        {
            new Film()
            {
                ID = 1,
                Filmnavn = "Death Wish",
                Filmbilde = "/Content/Images/deathwish.jpg",
                Beskrivelse = "Death Wish er en Action film",
                Pris = 49,
                Fremhevet = false,
                FremhevetBilde = "",
                Sjanger ="Action"
            },
            new Film()
            {
                ID = 2,
                Filmnavn = "Deadpool",
                Filmbilde = "/Content/Images/deadpool.jpg",
                Beskrivelse = "Deadpool er en Action film",
                Pris = 49,
                Fremhevet = false,
                FremhevetBilde = "",
                Sjanger ="Action"

            },
            new Film()
            {
                ID = 3,
                Filmnavn = "Deadpool 2",
                Filmbilde = "/Content/Images/deadpool2.jpg",
                Beskrivelse = "Deadpool 2 er en Action film",
                Pris = 49,
                Fremhevet = false,
                FremhevetBilde = "",
                Sjanger ="Action"
            }

        };
        public bool EndreFilm(Film innFilm)
        {
            if(innFilm.ID > 0
                && innFilm.Filmnavn != null
                && innFilm.Filmbilde != null
                && innFilm.Beskrivelse != null
                && innFilm.Pris >= 0
                && innFilm.Sjanger != null)
            {
                var film = filmer.Find(f => f.ID == innFilm.ID);
                if (film != null)
                {
                    film.Filmnavn = innFilm.Filmnavn;
                    film.Filmbilde = innFilm.Filmbilde;
                    film.Beskrivelse = innFilm.Beskrivelse;
                    film.Pris = innFilm.Pris;
                    film.Fremhevet = innFilm.Fremhevet;
                    film.FremhevetBilde = innFilm.FremhevetBilde;
                    film.Sjanger = innFilm.Sjanger;
                    return true;
                }
                return false;
            }
            return false;
            
        }

        public List<Film> HentAlle()
        { 
            return filmer;
        }

        public Film HentFilm(int id)
        {
            var film = filmer.Find( f => f.ID == id);
            if(film != null)
            {
                return film;
            }
            return null;
        
        }

        [ExcludeFromCodeCoverage]
        public List<Film> HentFilmerSjanger(int id)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        public List<JsonFilm> HentFilmSjangerJson(int id)
        {
            throw new NotImplementedException();
        }

        public bool OpprettFilm(Film nyFilm)
        {
            if(nyFilm != null 
                && nyFilm.ID > 0 
                && nyFilm.Filmnavn != null
                && nyFilm.Filmbilde != null
                && nyFilm.Sjanger != null)
            {
                filmer.Add(nyFilm);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SlettFilm(int id)
        {
            var film = filmer.Find(f => f.ID == id);
            if (film != null)
            {
                filmer.Remove(film);
                return true;
            }
            return false;
        }
    }
}
