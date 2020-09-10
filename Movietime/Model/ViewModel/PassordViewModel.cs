using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Model.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class PassordViewModel
    {
        [Display(Name = "Gammelt passord")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{5,}$", ErrorMessage = "Passord må ha hvertfall én stor bokstav, én liten bokstav, ett tall, og minimum 5 tegn.")]
        public string GammeltPassord { get; set; }

        [Display(Name = "Nytt passord")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{5,}$", ErrorMessage = "Passord må ha hvertfall én stor bokstav, én liten bokstav, ett tall, og minimum 5 tegn.")]
        public string NyttPassord { get; set; }

        [Display(Name = "Bekreft nytt passord")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{5,}$", ErrorMessage = "Passord må ha hvertfall én stor bokstav, én liten bokstav, ett tall, og minimum 5 tegn.")]
        public string NyttPassordBekreft { get; set; }
    }
}
