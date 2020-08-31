using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeastBook_final.Models
{
    public class Kategoria : IEnumerable<Kategoria>
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
        virtual public ICollection<Przepis> Przepis { get; set; }
        public int? NadKategoriaId { get; set; }
        [ForeignKey("NadKategoriaId")]
        virtual public Kategoria NadKategoria { get; set; }
        virtual public ICollection<Kategoria> PodKategorie { get; set; }

        public IEnumerator<Kategoria> GetEnumerator()
        {
            return PodKategorie.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return PodKategorie.GetEnumerator();
        }
    }
}
