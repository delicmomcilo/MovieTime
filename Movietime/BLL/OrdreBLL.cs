using BLL.Interfaces;
using DAL;
using Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BLL
{
    public class OrdreBLL : IOrdreBLL
    {
        private IDBOrdre dbOrdre;
        [ExcludeFromCodeCoverage]
        public OrdreBLL()
        {
            dbOrdre = new DBOrdre();

        }

        public OrdreBLL(IDBOrdre dBOrdre)
        {
            dbOrdre = dBOrdre;
        }

        public Ordre HentOrdre(int ordreID, int kID)
        {
            var ordre = dbOrdre.HentOrdre(ordreID, kID);
            return ordre;
        }
        [ExcludeFromCodeCoverage]
        public bool OpprettOrdre(HandleKurv kurv, int kID)
        {
            var OK = dbOrdre.OpprettOrdre(kurv, kID);

            if (OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        [ExcludeFromCodeCoverage]
        public HandleKurv LagHandleKurv(int kId)
        {
            var handleKurv = dbOrdre.LagHandleKurv(kId);
            return handleKurv;
            
            
        }
        [ExcludeFromCodeCoverage]
        public void LeggTilHandleKurv(HandleKurv kurv, int filmID)
        {
         
            dbOrdre.LeggTilHandleKurv(kurv, filmID);

        }
        [ExcludeFromCodeCoverage]
        public void FjernFraHandleKurv(HandleKurv kurv, int filmID)
        {
            dbOrdre.FjernFilm(kurv, filmID);
        }

   
        public List<Ordre> HentAlleOrdre(int kID)
        {
            
            return dbOrdre.HentAlleOrdre(kID);
        }

        public List<Ordre> HentAlleOrdre()
        {
            return dbOrdre.HentAlleOrdre();
        }

        public bool SlettOrdre(int oID)
        {
            return dbOrdre.SlettOrdre(oID);
        }
    }
}
