using IPBProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IPBProjekt.Data
{
    public class AppData : DbContext
    {
        public AppData(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Dokument> Dokumenty { get; set; }

        public DbSet<Kursant> Kursants { get; set; }

        public DbSet<IPBProjekt.Models.WydzialKomunikacji> WydzialKomunikacji { get; set; }

        public DbSet<IPBProjekt.Models.OsrodekSzkoleniaKierowcow> OsrodekSzkoleniaKierowcow { get; set; }

        public DbSet<Uzytkownik> Uzytkownicy { get; set; }

    }
}
