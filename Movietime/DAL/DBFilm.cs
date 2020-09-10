using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBFilm : IDBFilm
    {
        public int ID { get; set; }
        public string Filmnavn { get; set; }
        public double Pris { get; set; }
        public string Filmbilde { get; set; }
        public string Beskrivelse { get; set; }
        public bool Fremhevet { get; set; }
        public string FremhevetBilde { get; set; }

        public virtual List<DBSjanger> Sjanger { get; set; }
        public virtual List<DBOrdrelinje> OrdreLinjer { get; set; }

      
        private Film LastOppBilde(Film film) {

            
            if (film.BildeFil != null)
            {
             
                string filNavn = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(film.BildeFil.FileName);
                filNavn = filNavn + extension;
                film.Filmbilde = "/Content/Images/" + filNavn;
                filNavn = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/"), filNavn);
                film.BildeFil.SaveAs(filNavn);

            }

            if (film.FremhevetBildeFil != null)
            {
                string fremhevetFilNavn = Guid.NewGuid().ToString();
                string fremhevetExtension = Path.GetExtension(film.FremhevetBildeFil.FileName);
                fremhevetFilNavn = fremhevetFilNavn + fremhevetExtension;
                film.FremhevetBilde = "/Content/Images/" + fremhevetFilNavn;
                fremhevetFilNavn = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/"), fremhevetFilNavn);
                film.FremhevetBildeFil.SaveAs(fremhevetFilNavn);

            }
            return film;
        }
        public bool OpprettFilm(Film nyFilm)
        {
            try
            {
                using (var db = new DB())
                {

                    nyFilm = LastOppBilde(nyFilm);

                    var dbFilm = new DBFilm();
                    dbFilm.Filmnavn = nyFilm.Filmnavn;
                    dbFilm.Filmbilde = nyFilm.Filmbilde;
                    dbFilm.Beskrivelse = nyFilm.Beskrivelse;
                    dbFilm.Pris = nyFilm.Pris;
                    dbFilm.Fremhevet = nyFilm.Fremhevet;
                    dbFilm.FremhevetBilde = nyFilm.FremhevetBilde;

                    var filmSjanger = new List<DBSjanger>();
                    string[] sjangre = Regex.Split(nyFilm.Sjanger, " *, *");
                    foreach (var sjanger in sjangre)
                    {
                        var funnetSjanger = db.Sjangre.Where(s => s.sjanger == sjanger).FirstOrDefault();
                        if (funnetSjanger == null)
                        {
                            var nySjanger = new DBSjanger();
                            nySjanger.sjanger = sjanger;
                            filmSjanger.Add(nySjanger);
                            db.Sjangre.Add(nySjanger);
                        }
                        else
                        {
                            filmSjanger.Add(funnetSjanger);
                        }
                    }

                    dbFilm.Sjanger = filmSjanger;

                    db.Filmer.Add(dbFilm);
                    db.SaveChanges();
                    var endring = new DBEndring()
                    {
                        Tidspunkt = DateTime.Now,
                        EndringOperasjon = "En ny film har blitt opprettet: ",
                        endring = $"[{dbFilm.ID}] {dbFilm.Filmnavn} <br> {dbFilm.Beskrivelse} <br> <b> Fildestinasjon:</b> {dbFilm.Filmbilde} <br> <b>Fremhevet:</b>{dbFilm.Fremhevet} <br><b>Sjanger:</b>{nyFilm.Sjanger}"

                    };
                    db.Endringer.Add(endring);
                    db.SaveChanges();
                    return true;

                }

            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en film skulle opprettes", "DBFilm:OpprettFilm", ex);
                return false;
            }

        }
        public bool SlettFilm(int FilmID)
        {
            try
            {
                using(var db = new DB())
                {
                   var film = db.Filmer.Find(FilmID);
                   if(film != null)
                    {
                        var endringer = new DBEndring() {
                            Tidspunkt = DateTime.Now,
                            EndringOperasjon = "En film har blitt slettet: ",
                            endring = $"[{film.ID}] {film.Filmnavn}"

                        };
                        db.Filmer.Remove(film);
                        db.Endringer.Add(endringer);

                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }

            }catch(Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en film skulle fjernes", "DBFilm:SlettFilm", ex);
                return false;

            }
        }

      

        public bool EndreFilm(Film innFilm)
        {
            try
            {
                using (var db = new DB())
                {

                    var dbFilm = db.Filmer.Find(innFilm.ID);
                    if (dbFilm != null)
                    {
                        innFilm = LastOppBilde(innFilm);
                        dbFilm.Filmnavn = innFilm.Filmnavn;
                        dbFilm.Beskrivelse = innFilm.Beskrivelse;
                        dbFilm.Fremhevet = innFilm.Fremhevet;
                        dbFilm.Pris = innFilm.Pris;

                        if(innFilm.Filmbilde != null)
                        {
                            dbFilm.Filmbilde = innFilm.Filmbilde;

                        }
                        if (innFilm.Fremhevet == true && innFilm.FremhevetBilde != null)
                        {
                            dbFilm.FremhevetBilde = innFilm.FremhevetBilde;
                        }

                        var filmSjanger = dbFilm.Sjanger;
                        filmSjanger.Clear();
                        string[] sjangre = Regex.Split(innFilm.Sjanger, " *, *");
                        foreach (var sjanger in sjangre)
                        {
                            var funnetSjanger = db.Sjangre.Where(s => s.sjanger == sjanger).FirstOrDefault();
                            if (funnetSjanger == null)
                            {
                                var nySjanger = new DBSjanger();
                                nySjanger.sjanger = sjanger;
                                filmSjanger.Add(nySjanger);
                                db.Sjangre.Add(nySjanger);
                            }
                            else
                            {
                                filmSjanger.Add(funnetSjanger);
                            }
                        }

                        var endring = new DBEndring()
                        {
                            Tidspunkt = DateTime.Now,
                            EndringOperasjon = "En film har blitt endret: ",
                            endring = $"{innFilm.ID}, {innFilm.Filmnavn} {innFilm.Fremhevet}, {innFilm.Filmbilde}, {innFilm.Beskrivelse}"

                        };

                        db.Endringer.Add(endring);
                        dbFilm.Sjanger = filmSjanger;
                        db.SaveChanges();

                        return true;

                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en film skulle endres", "DBFilm:EndreFilm", ex);

                return false;

            }
        }
        public static string FraSjangerTilString(List<DBSjanger> sjangre)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < sjangre.Count; i++)
            {
                sb.Append(sjangre[i].sjanger);
                if (i != sjangre.Count - 1)
                {
                    sb.Append(", ");

                }
            }
            return sb.ToString();
        }

        public List<Film> HentAlle()
        {
            try
            {
                using (var db = new DB())
                {
                    
                    var dbFilmer = db.Filmer.ToList();
                    var domeneFilmer = new List<Film>();
                    foreach(var film in dbFilmer)
                    {
                        var domeneFilm = new Film();
                        var sjangerListe = new List<Sjanger>();

                        domeneFilm.ID = film.ID;
                        domeneFilm.Filmnavn = film.Filmnavn;
                        domeneFilm.Filmbilde = film.Filmbilde;
                        domeneFilm.FremhevetBilde = film.FremhevetBilde;
                        domeneFilm.Beskrivelse = film.Beskrivelse;
                        domeneFilm.Sjanger = FraSjangerTilString(film.Sjanger);
                        domeneFilm.Fremhevet = film.Fremhevet;
                        domeneFilm.Pris = film.Pris;
                        domeneFilmer.Add(domeneFilm);
          
                    }

                    return domeneFilmer;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når alle filmene skulle hentes", "DBFilm:HentAlle", ex);
                return null;
            }
            
        }

        public Film HentFilm(int id)
        {
            try
            {
                using(var db = new DB())
                {
                    DBFilm film = db.Filmer.Find(id);

                    return Mapper.Map<Film>(film);
                }

            }catch(Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en spesifik film skulle hentes", "DBFilm:HentFilm", ex);
                return null;
            }

        }

        public List<JsonFilm> HentFilmSjangerJson(int id)
        {
            try
            {
                using(var db = new DB())
                {

                    var dbFilmer = db.Filmer.ToList();
                    var jsonFilmList = new List<JsonFilm>();
                    foreach (var f in dbFilmer)
                    {
                        foreach(var s in f.Sjanger)
                        {
                            if(s.ID == id)
                            {
                                var jsonFilm = new JsonFilm();
                                jsonFilm.ID = f.ID;
                                jsonFilm.Filmnavn = f.Filmnavn;
                                jsonFilm.Pris = f.Pris;
                                jsonFilm.Filmbilde = f.Filmbilde;
                                jsonFilm.Beskrivelse = f.Beskrivelse;
                                jsonFilm.Sjanger = FraSjangerTilString(f.Sjanger);
                                jsonFilmList.Add(jsonFilm);
                            }
                        }
                        
                    }
                    return jsonFilmList;
                }
            }catch(Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når film sjanger skulle hentes med json", "DBFilm:HentFilmSjangerJson", ex);
                return null;
            }
        }
        


        public List<Film> HentFilmerSjanger(int id)
        {
            try
            {
                using (var db = new DB())
                {
                    var Filmer = db.Filmer.ToList();
                    var DomeneFilmer = new List<Film>();
                    foreach (var film in Filmer)
                    {
                        foreach(var sjanger in film.Sjanger)
                        {
                            if(sjanger.ID == id)
                            {
                                DomeneFilmer.Add(Mapper.Map<Film>(film));
                            }
                        }
                    }
                    return DomeneFilmer;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått filmsjangeren skulle hentes", "DBFilm:HentFilmerSjanger", ex);
                return null;
            }
        }
    }
}

   

public class JsonFilm
    {
        public int ID { get; set; }
        public string Filmnavn { get; set; }
        public double Pris { get; set; }
        public string Filmbilde { get; set; }
        public string Beskrivelse { get; set; }
        public string Sjanger { get; set; }

    }

   

