using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class KredittKortViewModel
    {
        [Display(Name = "Navn på kort")]
        [Required]
        [RegularExpression(@"^[A-Za-zæøåÆØÅöÖäÄëË\- ]+$", ErrorMessage = "Feltet inneholder ugyldige bokstaver.")]
        public string Kortholder { get; set; }

        [Display(Name = "Kortnummer")]
        [Required]
        [RegularExpression("^[1-9][0-9]{15}$", ErrorMessage = "Ugyldig kortnummer. Må være 16 tall, og kortnummer kan ikke starte på 0.")]
        public long Kortnummer { get; set; }

        [Display(Name = "CVV")]
        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Ugyldig CVV. Må være 3 tall.")]
        public int CVV { get; set; }

        [Display(Name = "Utløpsdato")]
        [Required]
        [RegularExpression("^[0-9]{2}-[0-9]{2}$", ErrorMessage = "Ugyldig utløpsdato. Må være av format MM-ÅÅ.")]
        public string Utlop { get; set; }
    }
}
