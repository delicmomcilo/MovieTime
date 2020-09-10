using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface IOrdreBLL
    {
        void FjernFraHandleKurv(HandleKurv kurv, int filmID);
        List<Ordre> HentAlleOrdre(int kID);
        List<Ordre> HentAlleOrdre();
        Ordre HentOrdre(int ordreID, int kID);
        HandleKurv LagHandleKurv(int kId);
        void LeggTilHandleKurv(HandleKurv kurv, int filmID);
        bool OpprettOrdre(HandleKurv kurv, int kID);
        bool SlettOrdre(int oID);
    }
}