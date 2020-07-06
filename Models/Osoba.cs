using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Models
{
    public abstract class Osoba
    {
        [Key]
        public int IdOsoba { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [JsonIgnore]
        public Uzytkownik Uzytkownik { get; set; }
    }
}
