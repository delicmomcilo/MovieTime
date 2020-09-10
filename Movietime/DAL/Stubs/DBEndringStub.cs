using System.Collections.Generic;
using Model;

namespace DAL.Stubs
{
    public class DBEndringStub : IDBEndring
    {
        private List<Endring> endringer = new List<Endring>();

        public List<Endring> HentAlle()
        {
            return endringer;
        }
    }
}
