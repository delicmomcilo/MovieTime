using BLL.Interfaces;
using DAL;
using Model;
using Model.ViewModel;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace BLL
{
    public class KundeBLL : IKundeBLL
    {
        private IDBKunde _dbKunde;


        [ExcludeFromCodeCoverage]
        public KundeBLL()
        {
            _dbKunde = new DBKunde();
        }

        public KundeBLL(IDBKunde dbKundeStub)
        {
            _dbKunde = dbKundeStub;

        }

        public bool Registrer(KundeRegistreringViewModel innKunde)
        {
            return _dbKunde.Registrer(innKunde);

        }
        [ExcludeFromCodeCoverage]
        public bool Login(KundeLoginViewModel innKunde)
        {     
            return _dbKunde.Login(innKunde);
         
        }
        [ExcludeFromCodeCoverage]
        public Kunde Finn(string epost)
        {
            return _dbKunde.Finn(epost);
        }
        [ExcludeFromCodeCoverage]
        public Kunde FinnKundeID(int id)
        {
            return _dbKunde.FinnKundeID(id);
        }
        
        public bool Endre(KundeEndreViewModel endretKunde, int kID)
        {
            return _dbKunde.Endre(endretKunde, kID);
        }
        [ExcludeFromCodeCoverage]
        public bool EndrePassord(PassordViewModel endretPassord, int kID)
        {
            return _dbKunde.EndrePassord(endretPassord, kID);
        }

        public List<KundeEndreViewModel> HentAlle(int MedRettighet)
        {
            return _dbKunde.HentAlle(MedRettighet);
        }

        public KundeEndreViewModel HentKunde(int kID)
        {
            return _dbKunde.HentKunde(kID);
        }

        public bool SlettKunde(int kID)
        {
            return _dbKunde.SlettKunde(kID);
        }

        public bool TildelRettigheter(int kID, int rettighet)
        {
            return _dbKunde.TildelRettigheter(kID, rettighet);
        }
    }

   
}
