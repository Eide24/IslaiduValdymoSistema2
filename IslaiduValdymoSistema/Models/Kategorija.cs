using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IslaiduValdymoSistema.Models
{
    public class Kategorija
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Įveskite kategorijos pavadinimą")]
        [StringLength(50, ErrorMessage = "Pavadinimas negali būti ilgesnis nei 50 simbolių")]
        [Display(Name = "Pavadinimas")]
        public string Pavadinimas { get; set; } = string.Empty;

        public List<Islaida> Islaidos { get; set; } = new List<Islaida>();
    }
}