using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Models
{
    public class Dokument
    {
        [Key]
        public int IdDokument { get; set; }

        public int? IdOsoba { get; set; }
        [ForeignKey("IdOsoba")]
        public Kursant Kursant { get; set; }

        public int? IdWydzialKomunikacji { get; set; }
        [ForeignKey("IdWydzialKomunikacji")]
        public WydzialKomunikacji WydzialKomunikacji { get; set; }
        public bool CzySprawdzony { get; set; } = false;
        public bool CzyPrzyjety { get; set; } = false;
        public bool CzyWyslany { get; set; } = false;

    }
}
