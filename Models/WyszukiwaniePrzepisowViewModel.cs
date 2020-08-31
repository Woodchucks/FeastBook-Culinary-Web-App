using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeastBook_final.Models
{
    public class WyszukiwaniePrzepisowViewModel
    {
        public List<Przepis> Przepisy { get; set; }
        public SelectList Kategorie { get; set; }
        public string WyszukanePrzepisy { get; set; }
        public string SearchString { get; set; }
    }
}
