using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Models
{
    public class WydzialKomunikacji
    {
        [Key]
        public int NumerWydzialu { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string NumerMieszkania { get; set; }
        [JsonIgnore]
        public IEnumerable<Uzytkownik> Uzytkownicy { get; set; }

        public IEnumerable<Dokument> Dokumenty { get; set; }

    }
}
