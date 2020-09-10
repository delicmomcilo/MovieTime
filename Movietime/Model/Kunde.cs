using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Model
{
    [ExcludeFromCodeCoverage]
    public class Kunde
    {

        public int ID { get; set; }

        public string Fornavn { get; set; }

        public string Etternavn { get; set; }

        public DateTime Fodselsdag { get; set; }

        public string Adresse { get; set; }

        public int Mobilnummer { get; set; }

        public string Epost { get; set; }
        public bool ErAdmin { get; set; }

        public string Postnummer { get; set; }

        public string Poststed { get; set; }
        public virtual List<Ordre> Ordrer { get; set; }


    }
}