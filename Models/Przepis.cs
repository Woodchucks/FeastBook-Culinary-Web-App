using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeastBook_final.Models
{
    public class Przepis
    {
        [Key]
        public int Id { get; set; }
        protected String _nazwa;
        [MaxLength(40)]
        [Display(Name = "Nazwa przepisu")]
        [Required(ErrorMessage = "Nazwa przepisu jest wymagana.")]
        public String Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }
        protected String _hasztag;
        [MaxLength(1000)]
        [Display(Name = "Hasztag")]
        [Required(ErrorMessage = "Hasztag jest wymagany.")]
        [RegularExpression(@"^#[\w-]+(?:\s+#[\w-]+)*$", ErrorMessage = "Wprowadzony hasztag jest niepoprawny, przykład poprawnego użycia /bez polskich znakow/: #pomidor #mleko")]
        public String Hasztag
        {
            get { return _hasztag; }
            set { _hasztag = value; }
        }
        protected int _ocena;
        [Display(Name = "Ocena")]
        [Required(ErrorMessage = "Ocena jest wymagana.")]
        [Range(1, 5, ErrorMessage = "Ocena musi być liczbą całkowitą z przedziału <1;5>, gdzie 1 - dostateczna, 5 - bardzo dobra")]
        public int Ocena
        {
            get { return _ocena; }
            set { _ocena = value; }
        }
        public byte[] Image { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }

        [Required(ErrorMessage = "Instrukcja wykonania przepisu jest wymagana.")]
        [Display(Name = "Instrukcja")]
        [MaxLength(1000)]
        protected String _tresc;
        public String Tresc
        {
            get { return _tresc; }
            set { _tresc = value; }
        }
        public int KategoriaId { get; set; }
        [ForeignKey("KategoriaId")]
        virtual public Kategoria Kategoria { get; set; }
        virtual public ICollection<PrzepisProdukt> Produkty { get; set; }
    }
}
