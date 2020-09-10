using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBOrdre
    {
        HandleKurv FjernFilm(HandleKurv kurv, int filmID);
        List<Ordre> HentAlleOrdre(int kID);
        List<Ordre> HentAlleOrdre();
        Ordre HentOrdre(int ordreID, int kID);
        double HentTotaltSalg();
        HandleKurv LagHandleKurv(int kID);
        bool LeggTilHandleKurv(HandleKurv kurv, int filmID);
        bool OpprettOrdre(HandleKurv kurv, int kID);
        bool SlettOrdre(int oID);
    }
}