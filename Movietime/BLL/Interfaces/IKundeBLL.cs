using System.Collections.Generic;
using Model;
using Model.ViewModel;

namespace BLL.Interfaces
{
    public interface IKundeBLL
    {
        bool Endre(KundeEndreViewModel endretKunde, int kID);
        bool EndrePassord(PassordViewModel endretPassord, int kID);
        Kunde Finn(string epost);
        Kunde FinnKundeID(int id);
        List<KundeEndreViewModel> HentAlle(int MedRettighet);
        bool Login(KundeLoginViewModel innKunde);
        bool Registrer(KundeRegistreringViewModel innKunde);
        bool SlettKunde(int kID);
        bool TildelRettigheter(int kID, int rettighet);

        KundeEndreViewModel HentKunde(int kID);
    }
}