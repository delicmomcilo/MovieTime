using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Model.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class KundeLoginViewModel
    {

        [Display(Name = "Epost")]
        [Required(ErrorMessage = "Epost må oppgis")]
        public string Epost { get; set; }


        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Passord { get; set; }

    }
}