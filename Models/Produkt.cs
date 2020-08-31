using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeastBook_final.Models
{
    public class Produkt
    {
        [Key]
        public int Id { get; set; }
        protected String _nazwa;
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public String Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }
        virtual public ICollection<PrzepisProdukt> Przepisy { get; set; }
    }
}
