using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace IPBProjekt.Models
{
    public class Kursant : Osoba
    {
        [Display(Name = "Numer PKK")]
        public int? NumerKursanta { get; set; }

        [Display(Name = " Adres email")]
        public string EmailAdress { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Data Urodzenia")]
        public DateTime DataUrodzenia { get; set; }

        public OsrodekSzkoleniaKierowcow OsrodekSzkoleniaKierowcow { get; set; }

        public Dokument Dokument { get; set; }


    }
}