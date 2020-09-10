using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBSjanger
    {
        List<Sjanger> HentAlleSjangre();
        List<JsonSjanger> HentAlleSjangreJson();
        bool LagSjanger(Sjanger sjanger);
    }
}