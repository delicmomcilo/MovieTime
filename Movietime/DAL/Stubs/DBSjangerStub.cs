using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model;

namespace DAL.Stubs
{
    public class DBSjangerStub : IDBSjanger
    {
        private List<Sjanger> sjangre = new List<Sjanger>()
        {
            new Sjanger()
            {
                ID = 1,
                sjanger = "Action"
            },
            new Sjanger()
            {
                ID = 2,
                sjanger = "SciFi"
            },
            new Sjanger()
            {
                ID = 3,
                sjanger = "Klassikere"
            }
        };
        [ExcludeFromCodeCoverage]
        public List<Sjanger> HentAlleSjangre()
        {
            throw new NotImplementedException();
        }
        [ExcludeFromCodeCoverage]
        public List<JsonSjanger> HentAlleSjangreJson()
        {
            throw new NotImplementedException();
        }

        public bool LagSjanger(Sjanger sjanger)
        {
            if(sjanger.sjanger != null)
            {
                var funnetSjanger = sjangre.Find( s => s.sjanger == sjanger.sjanger);
                if(funnetSjanger != null)
                {
                    return false;
                }
                else
                {
                    var nySjanger = new Sjanger();
                    nySjanger.ID = sjanger.ID;
                    nySjanger.sjanger = sjanger.sjanger;
                    return true;
              
                }
            }
            return false;
        }
    }
}
