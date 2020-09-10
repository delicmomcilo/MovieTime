using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBSjanger : IDBSjanger
    {
        public int ID { get; set; }
        public string sjanger { get; set; }
        public virtual List<DBFilm> Filmer { get; set; }

        public bool LagSjanger(Sjanger sjanger)
        {
            try
            {
                using (var db = new DB())
                {

                    var funnetSjanger = (from s in db.Sjangre
                                         where s.sjanger == sjanger.sjanger
                                         select s).FirstOrDefault();
                    if (funnetSjanger != null)
                    {
                        return false;
                    }
                    else
                    {
                        var nySjanger = new DBSjanger();
                        nySjanger.sjanger = sjanger.sjanger;
                        db.Sjangre.Add(nySjanger);
                        db.SaveChanges();
                        return true;

                    }
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en sjanger skulle lages", "DBSjanger:LagSjanger", ex);
                return false;
            }

        }
        public List<Sjanger> HentAlleSjangre()
        {
            try
            {
                using (var db = new DB())
                {
                    var alleSjangre = db.Sjangre.ToList();
                    var domeneSjanger = new List<Sjanger>();
                    foreach (var sjanger in alleSjangre)
                    {
                        domeneSjanger.Add(Mapper.Map<Sjanger>(sjanger));
                    }
                    return domeneSjanger;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når alle sjangre skulle hentes", "DBSjanger:HentAlleSjangre", ex);
                return null;
            }
        }

        public List<JsonSjanger> HentAlleSjangreJson()
        {
            try
            {
                using (var db = new DB())
                {
                    var alleSjangre = db.Sjangre.Select(s => new JsonSjanger
                    {
                        ID = s.ID,
                        sjanger = s.sjanger
                    }).ToList();

                    return alleSjangre;

                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når alle sjangre skulle hentes med Json", "DBSjanger:HentAlleSjangreJson", ex);
                return null;
            }
        }
    }

    public class JsonSjanger
    {
        public int ID { get; set; }
        public string sjanger { get; set; }
    }

}
