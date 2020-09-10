using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBEndring : IDBEndring
    {
        public int ID { get; set; }
        public string EndringOperasjon { get; set; }
        public string endring { get; set; }
        public DateTime Tidspunkt { get; set; }


        public List<Endring> HentAlle()
        {
            using (var db = new DB())
            {
                try
                {
                    var dbEndringer = db.Endringer.ToList();
                    var domeneEndringer = new List<Endring>();
                    foreach (var endring in dbEndringer)
                    {
                        domeneEndringer.Add(Mapper.Map<Endring>(endring));

                    }
                    domeneEndringer.Reverse();
                    return domeneEndringer;
                }
                catch (Exception e)
                {
                    DBLog.ErrorToFile("Feil oppstått når HentAlle-metoden prøve å hente alle endringene", "DBEndring:HentAlle", e);
                    return null;
                }
            }
        }
    }
}
