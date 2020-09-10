using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Model
{
    [ExcludeFromCodeCoverage]
    public class Poststed
    {
        public string Postnr { get; set; }
        public string poststed { get; set; }
        public virtual List<Kunde> Kunder { get; set; }
    }
}
