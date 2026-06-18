using System;
using System.ComponentModel.DataAnnotations;

namespace IslaiduValdymoSistema.Models
{
    public class Islaida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Įveskite išlaidos pavadinimą")]
        [StringLength(100, ErrorMessage = "Pavadinimas negali būti ilgesnis nei 100 simbolių")]
        [Display(Name = "Pavadinimas")]
        public string Pavadinimas { get; set; } = string.Empty;

        [Range(0.01, 100000, ErrorMessage = "Suma turi būti didesnė už 0")]
        [Display(Name = "Suma")]
        public decimal Suma { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Kategorija")]
        public int KategorijaId { get; set; }

        public Kategorija? Kategorija { get; set; }
    }
}