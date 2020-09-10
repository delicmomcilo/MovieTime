using System.Diagnostics.CodeAnalysis;


namespace Model
{
    [ExcludeFromCodeCoverage]
    public class HandleKurvLinje
    {
        
        public virtual Film Film { get; set; }
        public double Pris { get; set; }
        public int Antall { get; set; }


        public void KalkulerPris()
        {
            Pris = 0;
            Pris = Film.Pris * Antall;
        }
        

    }
}
