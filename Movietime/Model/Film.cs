using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Web;


namespace Model
{
    [ExcludeFromCodeCoverage]
    public class Film
    {
        public int ID { get; set; }
        [Required]
        public string Filmnavn { get; set; }
        [Required]
        public double Pris { get; set; }
        public string Filmbilde { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public string Sjanger { get; set; }
        [Required]
        public bool Fremhevet { get; set; }
        public string FremhevetBilde { get; set; }
        [Display(Name = "Bilde")]
        public HttpPostedFileBase BildeFil { get; set; }
        [Display(Name = "Fremhevet bilde")]
        public HttpPostedFileBase FremhevetBildeFil { get; set; }
        public virtual List<OrdreLinje> OrdreLinjer { get; set; }

        

    }

    
}