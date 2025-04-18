﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iteraBus.Repositorio.Contexto;

#nullable disable

namespace iteraBus.Repositorio.Migrations
{
    [DbContext(typeof(IteraBusContexto))]
    [Migration("20250320011124_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Localizacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LocalizacaoId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Horario")
                        .HasColumnType("datetime2")
                        .HasColumnName("Horario");

                    b.Property<double>("Latitude")
                        .HasPrecision(10, 6)
                        .HasColumnType("float(10)")
                        .HasColumnName("Longitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("OnibusId")
                        .HasColumnType("int")
                        .HasColumnName("OnibusId");

                    b.HasKey("Id");

                    b.HasIndex("OnibusId");

                    b.ToTable("Localização", null, t =>
                        {
                            t.Property("Longitude")
                                .HasColumnName("Longitude1");
                        });
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Onibus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OnibusId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Placa");

                    b.Property<int>("RotaId")
                        .HasColumnType("int")
                        .HasColumnName("Rota");

                    b.HasKey("Id");

                    b.HasIndex("RotaId");

                    b.ToTable("Onibus", (string)null);
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.PontoDeOnibus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PontoDeOnibusId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasColumnName("Latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasColumnName("Longitude");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<int>("RotaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RotaId");

                    b.ToTable("PontosDeOnibus", (string)null);
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Rota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RotaId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("Rotas", (string)null);
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Localizacao", b =>
                {
                    b.HasOne("iteraBus.Dominio.Entidades.Onibus", "Onibus")
                        .WithMany("Localizacoes")
                        .HasForeignKey("OnibusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Onibus");
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Onibus", b =>
                {
                    b.HasOne("iteraBus.Dominio.Entidades.Rota", "Rota")
                        .WithMany("Onibus")
                        .HasForeignKey("RotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rota");
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.PontoDeOnibus", b =>
                {
                    b.HasOne("iteraBus.Dominio.Entidades.Rota", "Rota")
                        .WithMany("PontosDeOnibus")
                        .HasForeignKey("RotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rota");
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Onibus", b =>
                {
                    b.Navigation("Localizacoes");
                });

            modelBuilder.Entity("iteraBus.Dominio.Entidades.Rota", b =>
                {
                    b.Navigation("Onibus");

                    b.Navigation("PontosDeOnibus");
                });
#pragma warning restore 612, 618
        }
    }
}
