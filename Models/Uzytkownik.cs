using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Models
{
    public class Uzytkownik
    {
        [Key]
        [JsonIgnore]
        public string Login { get; set; }
        [JsonIgnore]
        [DataType(DataType.Password)]
        public string Haslo { get; set; }
        public string Grupa { get; set; }
        public int? IdOsoba { get; set; }
        [ForeignKey("IdOsoba")]
        public Kursant Kursant { get; set; }

        public int? NumerWydzialu { get; set; }

        [ForeignKey("NumerWydzialu")]
        public WydzialKomunikacji WydzialKomunikacji { get; set; }

    }
}
