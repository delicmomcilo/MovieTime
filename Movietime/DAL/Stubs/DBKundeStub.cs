using System;
using System.Collections.Generic;
using Model;
using Model.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Stubs
{
    public class DBKundeStub : IDBKunde
    {
        private List<KundeEndreViewModel> kunder = new List<KundeEndreViewModel>()
        {
            new KundeEndreViewModel()
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
                ErAdmin = true
            },
            new KundeEndreViewModel()
            {
                ID = 2,
                Fornavn = "Momcilo",
                Etternavn = "Delic",
                Fodselsdag = new DateTime(1997, 01, 14),
                Adresse = "Vestlivingen 26",
                Poststed = "Oslo",
                Postnummer = "0969",
                Epost = "Momcilodelic@gmail.com",
                Mobilnummer = 47442316,
                ErAdmin = false
            },
            new KundeEndreViewModel()
            {
                ID = 3,
                Fornavn = "Fikret",
                Etternavn = "Kadiric",
                Fodselsdag = new DateTime(1993, 12, 25),
                Adresse = "Terningbekk 6",
                Poststed = "Oslo",
                Postnummer = "0272",
                Epost = "fikret@gmail.com",
                Mobilnummer = 94829458,
                ErAdmin = false
            }

        };

        public bool Endre(KundeEndreViewModel endretKunde, int id)
        {
            var funnetKunde = kunder.Find( k => k.ID == id);
            if(funnetKunde != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        [ExcludeFromCodeCoverage]
        public bool EndrePassord(PassordViewModel endretPassord, int kID)
        {
            throw new NotImplementedException();
        }
        [ExcludeFromCodeCoverage]
        public KundeEndreViewModel Finn(string epost)
        {
            throw new NotImplementedException();
        }
        [ExcludeFromCodeCoverage]
        public KundeEndreViewModel FinnKundeID(int id)
        {
            throw new NotImplementedException();

        }

        public List<KundeEndreViewModel> HentAlle(int MedRettighet)
        {
            var domeneKunder = new List<KundeEndreViewModel>();
            foreach (var kunde in kunder)
            {
                if (MedRettighet == 0 && kunde.ErAdmin == false)
                {
                    domeneKunder.Add(kunde);
                }
                else if (MedRettighet == 1 && kunde.ErAdmin == true)
                {
                    domeneKunder.Add(kunde);
                }
            }
            return domeneKunder;
        }

        [ExcludeFromCodeCoverage]
        public bool Login(KundeLoginViewModel innKunde)
        {
            throw new NotImplementedException();
        }

        public bool Registrer(KundeRegistreringViewModel innKunde)
        {
            if(innKunde.Fornavn != null
                && innKunde.Etternavn != null
                && innKunde.Fodselsdag != null
                && innKunde.Epost != null
                && innKunde.Adresse != null
                && innKunde.Mobilnummer != 0
                && innKunde.Passord != null
                && innKunde.Postnummer != null
                && innKunde.Poststed != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        [ExcludeFromCodeCoverage]
        Kunde IDBKunde.Finn(string epost)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        Kunde IDBKunde.FinnKundeID(int id)
        {
            throw new NotImplementedException();
        }

        public KundeEndreViewModel HentKunde(int kID)
        {
            if(kID > 0)
            {
                var kunde = kunder.Find(k => k.ID == kID);
                if(kunde != null)
                {
                    return kunde;
                }
                return null;
            }
            return null;

        }

        public bool SlettKunde(int kID)
        {
            if(kID > 0)
            {
                var kunde = kunder.Find(k => k.ID == kID);
                if(kunde != null)
                {
                    kunder.Remove(kunde);
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool TildelRettigheter(int kID, int rettighet)
        {
            var kunde = kunder.Find(k => k.ID == kID);

            if (kunde != null)
            {
                if (rettighet == 1)
                {
                    kunde.ErAdmin = true;
                    return true;
                }
                else
                {
                    kunde.ErAdmin = false;
                    return true;
                }
            }
            return false;
        }
    }
}
