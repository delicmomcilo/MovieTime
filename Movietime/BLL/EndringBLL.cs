using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BLL.Interfaces;
using DAL;
using Model;

namespace BLL
{
    public class EndringBLL : IEndringBLL
    {
        private IDBEndring dbEndring;


        [ExcludeFromCodeCoverage]
        public EndringBLL()
        {
            dbEndring = new DBEndring();
        }

        public EndringBLL(IDBEndring dbEndringStub)
        {
            dbEndring = dbEndringStub;
        }

        public List<Endring> Hent()
        {

            return dbEndring.HentAlle();
        }
    }
}
