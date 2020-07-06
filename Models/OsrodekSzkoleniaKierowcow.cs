using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Models
{
    public class OsrodekSzkoleniaKierowcow
    {
        [Key]
        public int IdOsrodka { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string NumerMieszkania { get; set; }

        public IEnumerable<Kursant> Kursants { get; set; }

    }
}
