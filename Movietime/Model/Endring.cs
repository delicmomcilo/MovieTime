using System;
using System.Diagnostics.CodeAnalysis;


namespace Model
{
    [ExcludeFromCodeCoverage]
    public class Endring
    {
        public int ID { get; set; }
        public string EndringOperasjon { get; set; }
        public string endring { get; set; }
        public DateTime Tidspunkt { get; set; }
    }
}
