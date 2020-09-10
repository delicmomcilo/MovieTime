using System.Collections.Generic;
using Model;
using Model.ViewModel;

namespace DAL
{
    public interface IDBKunde
    {
        bool Endre(KundeEndreViewModel endretKunde, int id);
        bool EndrePassord(PassordViewModel endretPassord, int kID);
        List<KundeEndreViewModel> HentAlle(int MedRettighet);
        bool Login(KundeLoginViewModel innKunde);
        bool Registrer(KundeRegistreringViewModel innKunde);
        bool SlettKunde(int kID);
        bool TildelRettigheter(int kID, int rettighet);
        Kunde Finn(string epost);
        Kunde FinnKundeID(int id);
        KundeEndreViewModel HentKunde(int kID);
    }
}