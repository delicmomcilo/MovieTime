using System.Diagnostics.CodeAnalysis;

namespace Model
{
    [ExcludeFromCodeCoverage]
    public class OrdreLinje
    {
        public int ID { get; set; }
        public int Antall { get; set; }
        public double Pris { get; set; }
        public virtual Film Film { get; set; }
        public virtual Ordre Ordre { get; set; }
       
    }
}
