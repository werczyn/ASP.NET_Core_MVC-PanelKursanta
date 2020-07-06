﻿// <auto-generated />
using System;
using IPBProjekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IPBProjekt.Migrations
{
    [DbContext(typeof(AppData))]
    partial class AppDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IPBProjekt.Models.Dokument", b =>
                {
                    b.Property<int>("IdDokument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CzyPrzyjety")
                        .HasColumnType("bit");

                    b.Property<bool>("CzySprawdzony")
                        .HasColumnType("bit");

                    b.Property<bool>("CzyWyslany")
                        .HasColumnType("bit");

                    b.Property<int?>("IdOsoba")
                        .HasColumnType("int");

                    b.Property<int?>("IdWydzialKomunikacji")
                        .HasColumnType("int");

                    b.HasKey("IdDokument");

                    b.HasIndex("IdOsoba")
                        .IsUnique()
                        .HasFilter("[IdOsoba] IS NOT NULL");

                    b.HasIndex("IdWydzialKomunikacji");

                    b.ToTable("Dokumenty");
                });

            modelBuilder.Entity("IPBProjekt.Models.Kursant", b =>
                {
                    b.Property<int>("IdOsoba")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAdress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumerKursanta")
                        .HasColumnType("int");

                    b.Property<int?>("OsrodekSzkoleniaKierowcowIdOsrodka")
                        .HasColumnType("int");

                    b.HasKey("IdOsoba");

                    b.HasIndex("OsrodekSzkoleniaKierowcowIdOsrodka");

                    b.ToTable("Kursants");
                });

            modelBuilder.Entity("IPBProjekt.Models.OsrodekSzkoleniaKierowcow", b =>
                {
                    b.Property<int>("IdOsrodka")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Miasto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumerMieszkania")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOsrodka");

                    b.ToTable("OsrodekSzkoleniaKierowcow");
                });

            modelBuilder.Entity("IPBProjekt.Models.Uzytkownik", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Grupa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Haslo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdOsoba")
                        .HasColumnType("int");

                    b.Property<int?>("NumerWydzialu")
                        .HasColumnType("int");

                    b.HasKey("Login");

                    b.HasIndex("IdOsoba")
                        .IsUnique()
                        .HasFilter("[IdOsoba] IS NOT NULL");

                    b.HasIndex("NumerWydzialu");

                    b.ToTable("Uzytkownicy");
                });

            modelBuilder.Entity("IPBProjekt.Models.WydzialKomunikacji", b =>
                {
                    b.Property<int>("NumerWydzialu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Miasto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumerMieszkania")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NumerWydzialu");

                    b.ToTable("WydzialKomunikacji");
                });

            modelBuilder.Entity("IPBProjekt.Models.Dokument", b =>
                {
                    b.HasOne("IPBProjekt.Models.Kursant", "Kursant")
                        .WithOne("Dokument")
                        .HasForeignKey("IPBProjekt.Models.Dokument", "IdOsoba");

                    b.HasOne("IPBProjekt.Models.WydzialKomunikacji", "WydzialKomunikacji")
                        .WithMany("Dokumenty")
                        .HasForeignKey("IdWydzialKomunikacji");
                });

            modelBuilder.Entity("IPBProjekt.Models.Kursant", b =>
                {
                    b.HasOne("IPBProjekt.Models.OsrodekSzkoleniaKierowcow", "OsrodekSzkoleniaKierowcow")
                        .WithMany("Kursants")
                        .HasForeignKey("OsrodekSzkoleniaKierowcowIdOsrodka");
                });

            modelBuilder.Entity("IPBProjekt.Models.Uzytkownik", b =>
                {
                    b.HasOne("IPBProjekt.Models.Kursant", "Kursant")
                        .WithOne("Uzytkownik")
                        .HasForeignKey("IPBProjekt.Models.Uzytkownik", "IdOsoba");

                    b.HasOne("IPBProjekt.Models.WydzialKomunikacji", "WydzialKomunikacji")
                        .WithMany("Uzytkownicy")
                        .HasForeignKey("NumerWydzialu");
                });
#pragma warning restore 612, 618
        }
    }
}
