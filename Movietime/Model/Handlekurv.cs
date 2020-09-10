using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    [ExcludeFromCodeCoverage]
    public class HandleKurv
    {

        public DateTime Dato { get; set; }
        public virtual List<HandleKurvLinje> HandleKurvLinjer { get; set; }
        public int Antall { get; set; }
        public double TotalPris { get; set; }

        public void KalkulerTotalPris()
        {
            TotalPris = 0;

            foreach (var vareLinje in HandleKurvLinjer)
            {
                TotalPris += vareLinje.Film.Pris * vareLinje.Antall;
            }
        }

    }

    

  
}
