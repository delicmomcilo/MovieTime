using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBPoststed
    {

        [Key]
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public virtual List<DBKunde> Kunder { get; set; }
    }
}
