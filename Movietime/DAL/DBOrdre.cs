using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBOrdre : IDBOrdre
    {
        public int ID { get; set; }
        public DateTime Dato { get; set; }
        public double TotalPris { get; set; }
        public virtual DBKunde Kunde { get; set; }
        public virtual List<DBOrdrelinje> OrdreLinjer { get; set; }

        public bool OpprettOrdre(HandleKurv kurv, int kID)
        {
            try
            { 
                if(kurv != null)
                {
                    using(var db = new DB())
                    {
                        var dbKunde = db.Kunder.Find(kID);
                        // Lager ordre 
                        var ordre = new DBOrdre();
                        ordre.Dato = DateTime.Now;
                        // Lager ordrelinjer som blir satt inn i OrdeLinje senere
                        var ordreLinjer = new List<DBOrdrelinje>();

                    
                        foreach (var vareLinje in kurv.HandleKurvLinjer)
                        {
                            var ordreLinje = new DBOrdrelinje();
                            var dbFilm = db.Filmer.Find(vareLinje.Film.ID);
                            ordreLinje.Film = dbFilm;
                            ordreLinje.Antall = vareLinje.Antall;
                            ordreLinje.Pris = vareLinje.Pris;
                            ordreLinjer.Add(ordreLinje);

                            //Legger ordrelinje i databasen
                            db.OrdreLinjer.Add(ordreLinje);
                        }
                        ordre.TotalPris = kurv.TotalPris;
                        ordre.OrdreLinjer = ordreLinjer;
                        var kundeOrdrer = new List<DBOrdre>();
                        kundeOrdrer.Add(ordre);

                        var endring = new DBEndring()
                        {
                            Tidspunkt = DateTime.Now,
                            EndringOperasjon = "En ny ordre har blitt opprettet av kunde med følgende ID: ",
                            endring = $"{kID}"

                        };

                        dbKunde.Ordrer = kundeOrdrer;
                        db.Endringer.Add(endring);
                        db.Ordrer.Add(ordre);
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når en ordre skulle opprettes", "DBKunde:OpprettOrdre", e);
                return false;
            }
        }

        public HandleKurv LagHandleKurv(int kID)
        {
            var handleKurv = new HandleKurv();
            handleKurv.Antall = 0;
            handleKurv.Dato = DateTime.Now;
            handleKurv.TotalPris = 0;
            var handleKurvLinjer = new List<HandleKurvLinje>();
            handleKurv.HandleKurvLinjer = handleKurvLinjer;

            return handleKurv;

        }

        public bool LeggTilHandleKurv(HandleKurv kurv, int filmID)
        {
            try
            { 
                var handleKurvLinje = new HandleKurvLinje();
                using(var db = new DB())
                {
                    var film = db.Filmer.Find(filmID);
                    kurv.Antall += 1; 
                    if(kurv.HandleKurvLinjer != null)
                    {
                        bool duplikat = false;
                        foreach (var vareLinje in kurv.HandleKurvLinjer)
                        {
                            if (vareLinje.Film.ID == film.ID)
                            {
                                vareLinje.Antall += 1;
                                vareLinje.KalkulerPris();
                                kurv.KalkulerTotalPris();
                            
                                duplikat = true;
                            }
                        }
                        if (!duplikat)
                        {
                            handleKurvLinje.Film = Mapper.Map<Film>(film);
                            handleKurvLinje.Antall = 1;
                            handleKurvLinje.Pris = film.Pris;
                            handleKurvLinje.KalkulerPris();
                            kurv.HandleKurvLinjer.Add(handleKurvLinje);
                            kurv.KalkulerTotalPris();


                            duplikat = false;
                        }

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når en film skulle legges til handlekurv", "DBKunde:LeggTilHandleKurv", e);
                return false;
            }
        }

        public HandleKurv FjernFilm(HandleKurv kurv, int filmID)
        {
            try
            { 
                foreach(var vareLinje in kurv.HandleKurvLinjer)
                {           
                    if(vareLinje.Film.ID == filmID)
                    {
                        if(vareLinje.Antall > 1)
                        {
                            vareLinje.Antall -= 1; 
                        
                        }
                        else
                        {
                            kurv.HandleKurvLinjer.Remove(vareLinje);
                        }
                        kurv.Antall -= 1;
                        vareLinje.KalkulerPris();
                        kurv.KalkulerTotalPris();

                        return kurv;
                    }
                }
                return kurv;
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når en film skulle fjernes fra handlekurven", "DBKunde:FjernFilm", e);
                return null;
            }
        }

        public List<Ordre> HentAlleOrdre(int kID)
        {
            try
            {
                using (var db = new DB())
                {
                    var alleOrdre = new List<Ordre>();
                    var kundeOrdre = db.Kunder.Find(kID).Ordrer;
                    foreach (var kOrdre in kundeOrdre)
                    {
                        alleOrdre.Add(Mapper.Map<Ordre>(kOrdre));
                    }
                    return alleOrdre;
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når alle ordrene til kunde skulle hentes", "DBKunde:HentAlleOrdre", e);
                return null;
            }
        }

        public List<Ordre> HentAlleOrdre()
        {
            try
            {
                using(var db = new DB())
                {
                    var dbOrdre = db.Ordrer.ToList();
                    var alleOrdre = new List<Ordre>();
                    if(dbOrdre != null)
                    {
                        foreach (var ordre in dbOrdre)
                        {
                            alleOrdre.Add(Mapper.Map<Ordre>(ordre));
                        }
                    }
                    
                    return alleOrdre;
                }

            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når alle ordrene skulle hentes", "DBKunde:HentAlleOrdre", ex);
                return null;
            }
        }

        public Ordre HentOrdre(int ordreID, int kID)
        {
            try
            {
                using (var db = new DB())
                {
                    var ordre = db.Ordrer.Find(ordreID);

                    //Sjekker om ordre tilhører kunde
                    if (ordre.Kunde.ID == kID)
                    {
                        return Mapper.Map<Ordre>(ordre);

                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når data fra en spesifik ordre skulle hentes", "DBKunde:HentOrdre", e);
                return null;
            }
        }

        public double HentTotaltSalg()
        {
            try
            {
                using (var db = new DB())
                {
                    var ordrer = db.Ordrer.ToList();
                    double totalSalg = 0;
                    foreach(var ordre in ordrer)
                    {
                        totalSalg += ordre.TotalPris;
                    }
                    return totalSalg;
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når totalt antall salg skulle hentes", "DBKunde:HentTotaltSalg", e);
                return 0;
            }
        }

        public bool SlettOrdre(int oID)
        {
            try
            {
                using(var db = new DB())
                {
                    var ordre = db.Ordrer.Find(oID);
                    if(ordre != null)
                    {
                        var kunde = db.Kunder.Find(ordre.Kunde.ID);
                        //ordre.OrdreLinjer.Clear();
                        //kunde.Ordrer.Remove(ordre);
                     
                        foreach (var ordrelinje in ordre.OrdreLinjer.ToList())
                        {
                            db.OrdreLinjer.Remove(ordrelinje);
                        }


                        var endring = new DBEndring()
                        {
                            Tidspunkt = DateTime.Now,
                            EndringOperasjon = "Ordre med følgende ID:",
                            endring = $"{ordre.ID} er slettet"

                        };
                        db.Ordrer.Remove(ordre);
                        db.Endringer.Add(endring);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått ved sletting av ordre", "DBKunde:SlettOrdre", ex);
                return false;
            }
        }
    }
}
