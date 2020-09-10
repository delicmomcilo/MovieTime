using System.Diagnostics.CodeAnalysis;


namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBOrdrelinje
    {
        public int ID { get; set; }
        public int Antall { get; set; }
        public double Pris { get; set; }
        public virtual DBFilm Film { get; set; }
        public virtual DBOrdre Ordre { get; set; }
    }
}
