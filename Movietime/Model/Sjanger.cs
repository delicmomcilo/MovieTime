using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    [ExcludeFromCodeCoverage]
    public class Sjanger
    {
        public int ID { get; set; }
        [Display(Name = "Sjanger")]
        [Required(ErrorMessage = "Sjanger må oppgis")]
        [RegularExpression(@"^[A-Za-zæøåÆØÅ\- ]+$", ErrorMessage = "Sjanger kan kun være bokstaver")]
        public string sjanger { get; set; }
        public virtual List<Film> Filmer {get; set;}
    }

}
