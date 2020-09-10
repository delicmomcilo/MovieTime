using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface ISjangerBLL
    {
        string HentAlleSjangreJson();
        List<Film> HentFilmerSjanger(int id);
        string HentFilmerSjangerJson(int id);
        bool LagSjanger(Sjanger sjanger);
    }
}