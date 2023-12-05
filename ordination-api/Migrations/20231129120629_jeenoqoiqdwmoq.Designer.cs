﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ordination_api.Migrations
{
    [DbContext(typeof(OrdinationContext))]
    [Migration("20231129120629_jeenoqoiqdwmoq")]
    partial class jeenoqoiqdwmoq
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("shared.Model.Dato", b =>
                {
                    b.Property<int>("DatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PNOrdinationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("dato")
                        .HasColumnType("TEXT");

                    b.HasKey("DatoId");

                    b.HasIndex("PNOrdinationId");

                    b.ToTable("Dato");
                });

            modelBuilder.Entity("shared.Model.Dosis", b =>
                {
                    b.Property<int>("DosisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DagligSkævOrdinationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("antal")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("tid")
                        .HasColumnType("TEXT");

                    b.HasKey("DosisId");

                    b.HasIndex("DagligSkævOrdinationId");

                    b.ToTable("Dosis");
                });

            modelBuilder.Entity("shared.Model.Laegemiddel", b =>
                {
                    b.Property<int>("LaegemiddelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("enhed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("enhedPrKgPrDoegnLet")
                        .HasColumnType("REAL");

                    b.Property<double>("enhedPrKgPrDoegnNormal")
                        .HasColumnType("REAL");

                    b.Property<double>("enhedPrKgPrDoegnTung")
                        .HasColumnType("REAL");

                    b.Property<string>("navn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LaegemiddelId");

                    b.ToTable("Laegemiddler");
                });

            modelBuilder.Entity("shared.Model.Ordination", b =>
                {
                    b.Property<int>("OrdinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LaegemiddelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("slutDen")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("startDen")
                        .HasColumnType("TEXT");

                    b.HasKey("OrdinationId");

                    b.HasIndex("LaegemiddelId");

                    b.HasIndex("PatientId");

                    b.ToTable("Ordinationer");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Ordination");
                });

            modelBuilder.Entity("shared.Model.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cprnr")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("navn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("vaegt")
                        .HasColumnType("REAL");

                    b.HasKey("PatientId");

                    b.ToTable("Patienter");
                });

            modelBuilder.Entity("shared.Model.DagligFast", b =>
                {
                    b.HasBaseType("shared.Model.Ordination");

                    b.Property<int>("AftenDosisDosisId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MiddagDosisDosisId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MorgenDosisDosisId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NatDosisDosisId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("AftenDosisDosisId");

                    b.HasIndex("MiddagDosisDosisId");

                    b.HasIndex("MorgenDosisDosisId");

                    b.HasIndex("NatDosisDosisId");

                    b.HasDiscriminator().HasValue("DagligFast");
                });

            modelBuilder.Entity("shared.Model.DagligSkæv", b =>
                {
                    b.HasBaseType("shared.Model.Ordination");

                    b.HasDiscriminator().HasValue("DagligSkæv");
                });

            modelBuilder.Entity("shared.Model.PN", b =>
                {
                    b.HasBaseType("shared.Model.Ordination");

                    b.Property<double>("antalEnheder")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("PN");
                });

            modelBuilder.Entity("shared.Model.Dato", b =>
                {
                    b.HasOne("shared.Model.PN", null)
                        .WithMany("dates")
                        .HasForeignKey("PNOrdinationId");
                });

            modelBuilder.Entity("shared.Model.Dosis", b =>
                {
                    b.HasOne("shared.Model.DagligSkæv", null)
                        .WithMany("doser")
                        .HasForeignKey("DagligSkævOrdinationId");
                });

            modelBuilder.Entity("shared.Model.Ordination", b =>
                {
                    b.HasOne("shared.Model.Laegemiddel", "laegemiddel")
                        .WithMany()
                        .HasForeignKey("LaegemiddelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shared.Model.Patient", null)
                        .WithMany("ordinationer")
                        .HasForeignKey("PatientId");

                    b.Navigation("laegemiddel");
                });

            modelBuilder.Entity("shared.Model.DagligFast", b =>
                {
                    b.HasOne("shared.Model.Dosis", "AftenDosis")
                        .WithMany()
                        .HasForeignKey("AftenDosisDosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shared.Model.Dosis", "MiddagDosis")
                        .WithMany()
                        .HasForeignKey("MiddagDosisDosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shared.Model.Dosis", "MorgenDosis")
                        .WithMany()
                        .HasForeignKey("MorgenDosisDosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shared.Model.Dosis", "NatDosis")
                        .WithMany()
                        .HasForeignKey("NatDosisDosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AftenDosis");

                    b.Navigation("MiddagDosis");

                    b.Navigation("MorgenDosis");

                    b.Navigation("NatDosis");
                });

            modelBuilder.Entity("shared.Model.Patient", b =>
                {
                    b.Navigation("ordinationer");
                });

            modelBuilder.Entity("shared.Model.DagligSkæv", b =>
                {
                    b.Navigation("doser");
                });

            modelBuilder.Entity("shared.Model.PN", b =>
                {
                    b.Navigation("dates");
                });
#pragma warning restore 612, 618
        }
    }
}
