using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model;

namespace DAL.Stubs
{
    public class DBOrdreStub : IDBOrdre
    {
        private List<Ordre> ordrer = new List<Ordre>()
        {
            new Ordre()
            {
                ID = 1,
                Kunde = new Kunde()
                {
                    ID = 2,
                    Fornavn = "Momcilo",
                    Etternavn = "Delic",
                    Fodselsdag = new DateTime(1997, 12, 25),
                    Adresse = "Terningbekk 9",
                    Poststed = "Oslo",
                    Postnummer = "0171",
                    Epost = "Momcilo@gmail.com",
                    Mobilnummer = 41379773,
                },
                Dato = new DateTime(2018,10,20),
                OrdreLinjer = new List<OrdreLinje>(),
                TotalPris = 100
            },
            new Ordre()
            {
                ID = 2,
                Kunde = new Kunde()
                {
                    ID = 1,
                    Fornavn = "Henry",
                    Etternavn = "Tran",
                    Fodselsdag = new DateTime(1997, 12, 25),
                    Adresse = "Brageveien 9",
                    Poststed = "Oslo",
                    Postnummer = "0171",
                    Epost = "Henry.nor@gmail.com",
                    Mobilnummer = 41379773,
                },
                Dato = new DateTime(2018,10,21),
                OrdreLinjer = new List<OrdreLinje>(),
                TotalPris = 100
            },
            new Ordre()
            {
                ID = 3,
                Kunde = new Kunde()
                {
                    ID = 3,
                    Fornavn = "Fikret",
                    Etternavn = "Kadiric",
                    Fodselsdag = new DateTime(1997, 12, 25),
                    Adresse = "Terningbekk 9",
                    Poststed = "Oslo",
                    Postnummer = "0171",
                    Epost = "Fikret@gmail.com",
                    Mobilnummer = 41379773,
                },
                Dato = new DateTime(2018,10,22),
                OrdreLinjer = new List<OrdreLinje>(),
                TotalPris = 100
            }
        };
        [ExcludeFromCodeCoverage]
        public HandleKurv FjernFilm(HandleKurv kurv, int filmID)
        {
            throw new NotImplementedException();
        }

        public List<Ordre> HentAlleOrdre(int kID)
        {
            var listeOrdre = new List<Ordre>();
            foreach (var ordre in ordrer)
            {
                if(ordre.Kunde.ID == kID)
                {
                    listeOrdre.Add(ordre);
                }
            }
            return listeOrdre;
        }

        public List<Ordre> HentAlleOrdre()
        {
            return ordrer;
        }

        public Ordre HentOrdre(int ordreID, int kID)
        {
            var ordre = ordrer.Find(o => o.ID == ordreID);
            if(ordre.Kunde.ID == kID)
            {
                return ordre;
            }
            return null;
        }

        public double HentTotaltSalg()
        {
            double totalSalg = 0;
            foreach (var ordre in ordrer)
            {
                totalSalg += ordre.TotalPris;

            }
            return totalSalg;
        }

        [ExcludeFromCodeCoverage]
        public HandleKurv LagHandleKurv(int kID)
        {
            throw new NotImplementedException();
        }
        [ExcludeFromCodeCoverage]
        public bool LeggTilHandleKurv(HandleKurv kurv, int filmID)
        {
            throw new NotImplementedException();
        }
        [ExcludeFromCodeCoverage]
        public bool OpprettOrdre(HandleKurv kurv, int kID)
        {
            throw new NotImplementedException();
        }

        public bool SlettOrdre(int oID)
        {
            var ordre = ordrer.Find(o => o.ID == oID);
            if(ordre != null)
            {
                ordrer.Remove(ordre);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
