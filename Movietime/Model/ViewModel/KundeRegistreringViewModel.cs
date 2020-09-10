using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Model.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class KundeRegistreringViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"^[A-Za-zæøåÆØÅ\- ]+$", ErrorMessage = "Fornavn kan kun være bokstaver")]
        public string Fornavn { get; set; }
        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"^[A-Za-zæøåÆØÅ\- ]+$", ErrorMessage = "Etternavn kan kun være bokstaver")]
        public string Etternavn { get; set; }
        [Display(Name = "Fodselsdag")]
        [Required(ErrorMessage = "Fodselsdag må oppgis")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fodselsdag { get; set; }
        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public string Adresse { get; set; }
        [Display(Name = "Mobilnummer")]
        [Required(ErrorMessage = "Mobilnummer må oppgis")]
        public int Mobilnummer { get; set; }

        [Display(Name = "Epost")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Epost må oppgis")]
        [RegularExpression(@"^[A-Za-zæøåÆØÅ0-9_\-,\. ]+@[a-zA-Z0-9]+\.[a-zA-Z]+$", ErrorMessage = "Ugyldig e-post")]
        public string Epost { get; set; }
        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}")]
        public string Postnummer { get; set; }
        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string Poststed { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{5,}$", ErrorMessage = "Passord må ha hvertfall én stor bokstav, én liten bokstav, ett tall, og minimum 5 tegn.")]
        public string Passord { get; set; }
    }
}
