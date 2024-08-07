﻿// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BackEnd.Models.ApointmentTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMeet")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("IdDoctor")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdRegisterTicket")
                        .HasColumnType("int");

                    b.Property<int>("IdTimeMeet")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdRegisterTicket")
                        .IsUnique();

                    b.HasIndex("IdTimeMeet");

                    b.ToTable("ApointmentTickets");
                });

            modelBuilder.Entity("BackEnd.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BgDisease")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdDepartment")
                        .HasColumnType("int");

                    b.Property<int?>("IdTest")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IdDepartment");

                    b.HasIndex("IdTest");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0928796f-1f66-4036-8bcf-5115a0ddd3b8",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEA0P+GziuySxexgeTuDCS5vle8fCEGPfIYWz2Q8gPhlDzR7E1/4w/DoMmyFYcID+yg==",
                            PhoneNumber = "1234567890",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "da1dde0a-aff4-4d63-9ca0-bc55a54ecc72",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        });
                });

            modelBuilder.Entity("BackEnd.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Datecreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Datecreate = new DateTime(2024, 1, 24, 15, 43, 18, 960, DateTimeKind.Local).AddTicks(2061),
                            Decription = "Cột Sống",
                            Deleted = false,
                            Name = "Cột Sống"
                        },
                        new
                        {
                            Id = 2,
                            Datecreate = new DateTime(2024, 1, 24, 15, 43, 18, 960, DateTimeKind.Local).AddTicks(2645),
                            Decription = "Nội Tiết",
                            Deleted = false,
                            Name = "Nội Tiết"
                        },
                        new
                        {
                            Id = 3,
                            Datecreate = new DateTime(2024, 1, 24, 15, 43, 18, 960, DateTimeKind.Local).AddTicks(2648),
                            Decription = "Mắt",
                            Deleted = false,
                            Name = "Mắt"
                        },
                        new
                        {
                            Id = 4,
                            Datecreate = new DateTime(2024, 1, 24, 15, 43, 18, 960, DateTimeKind.Local).AddTicks(2649),
                            Decription = "Da Liễu",
                            Deleted = false,
                            Name = "Da Liễu"
                        });
                });

            modelBuilder.Entity("BackEnd.Models.RegisterTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMeet")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("IdPatient")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdTimeMeet")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Symptom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPatient");

                    b.HasIndex("IdTimeMeet");

                    b.ToTable("RegisterTickets");
                });

            modelBuilder.Entity("BackEnd.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Diagnostic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdApointmentTicket")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TherapyRegiment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdApointmentTicket")
                        .IsUnique()
                        .HasFilter("[IdApointmentTicket] IS NOT NULL");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("BackEnd.Models.ResultDetail", b =>
                {
                    b.Property<int>("IdResult")
                        .HasColumnType("int");

                    b.Property<string>("IdDoctorTest")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnostic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlFile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdResult", "IdDoctorTest");

                    b.HasIndex("IdDoctorTest");

                    b.ToTable("ResultDetails");
                });

            modelBuilder.Entity("BackEnd.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("Datecreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("BackEnd.Models.TimeSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Max")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeSlot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreate = new DateTime(2024, 1, 24, 15, 43, 18, 958, DateTimeKind.Local).AddTicks(9966),
                            Decription = "6h-9h",
                            Deleted = false,
                            Max = 100
                        },
                        new
                        {
                            Id = 2,
                            DateCreate = new DateTime(2024, 1, 24, 15, 43, 18, 959, DateTimeKind.Local).AddTicks(5765),
                            Decription = "9h-12h",
                            Deleted = false,
                            Max = 100
                        },
                        new
                        {
                            Id = 3,
                            DateCreate = new DateTime(2024, 1, 24, 15, 43, 18, 959, DateTimeKind.Local).AddTicks(5774),
                            Decription = "13h-15h",
                            Deleted = false,
                            Max = 100
                        },
                        new
                        {
                            Id = 4,
                            DateCreate = new DateTime(2024, 1, 24, 15, 43, 18, 959, DateTimeKind.Local).AddTicks(5776),
                            Decription = "15h-17h",
                            Deleted = false,
                            Max = 100
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "01",
                            ConcurrencyStamp = "626bbf5e-26d3-4874-aedc-9494621710c8",
                            Name = "Doctor Test",
                            NormalizedName = "Doctor Test"
                        },
                        new
                        {
                            Id = "02",
                            ConcurrencyStamp = "865528f4-3f0f-444b-8837-45f7b7579de7",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "03",
                            ConcurrencyStamp = "eaf10c23-d90b-4d8a-924e-15c30d72e9a7",
                            Name = "Patient",
                            NormalizedName = "Patient"
                        },
                        new
                        {
                            Id = "04",
                            ConcurrencyStamp = "1e1b8aca-c4fd-4060-8ba6-320d0ea31834",
                            Name = "Doctor Examines",
                            NormalizedName = "Doctor Examines"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
                            RoleId = "02"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BackEnd.Models.ApointmentTicket", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", "Doctor")
                        .WithMany("ApointmentTickets")
                        .HasForeignKey("IdDoctor");

                    b.HasOne("BackEnd.Models.RegisterTicket", "registerticket")
                        .WithOne("apointmentTicket")
                        .HasForeignKey("BackEnd.Models.ApointmentTicket", "IdRegisterTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.TimeSlot", "TimeSlot")
                        .WithMany("ApointmentTickets")
                        .HasForeignKey("IdTimeMeet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("registerticket");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("BackEnd.Models.ApplicationUser", b =>
                {
                    b.HasOne("BackEnd.Models.Department", "department")
                        .WithMany("applicationUsers")
                        .HasForeignKey("IdDepartment");

                    b.HasOne("BackEnd.Models.Test", "Test")
                        .WithMany("DoctorTests")
                        .HasForeignKey("IdTest");

                    b.Navigation("department");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BackEnd.Models.RegisterTicket", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", "User")
                        .WithMany("registerTickets")
                        .HasForeignKey("IdPatient");

                    b.HasOne("BackEnd.Models.TimeSlot", "Timeslot")
                        .WithMany("RegisterTickets")
                        .HasForeignKey("IdTimeMeet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Timeslot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackEnd.Models.Result", b =>
                {
                    b.HasOne("BackEnd.Models.ApointmentTicket", "ApointmentTicket")
                        .WithOne("Result")
                        .HasForeignKey("BackEnd.Models.Result", "IdApointmentTicket");

                    b.Navigation("ApointmentTicket");
                });

            modelBuilder.Entity("BackEnd.Models.ResultDetail", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", "DoctorTest")
                        .WithMany("ResultDetails1")
                        .HasForeignKey("IdDoctorTest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Result", "Result")
                        .WithMany("ResultDetails2")
                        .HasForeignKey("IdResult")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoctorTest");

                    b.Navigation("Result");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BackEnd.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.ApointmentTicket", b =>
                {
                    b.Navigation("Result");
                });

            modelBuilder.Entity("BackEnd.Models.ApplicationUser", b =>
                {
                    b.Navigation("ApointmentTickets");

                    b.Navigation("registerTickets");

                    b.Navigation("ResultDetails1");
                });

            modelBuilder.Entity("BackEnd.Models.Department", b =>
                {
                    b.Navigation("applicationUsers");
                });

            modelBuilder.Entity("BackEnd.Models.RegisterTicket", b =>
                {
                    b.Navigation("apointmentTicket");
                });

            modelBuilder.Entity("BackEnd.Models.Result", b =>
                {
                    b.Navigation("ResultDetails2");
                });

            modelBuilder.Entity("BackEnd.Models.Test", b =>
                {
                    b.Navigation("DoctorTests");
                });

            modelBuilder.Entity("BackEnd.Models.TimeSlot", b =>
                {
                    b.Navigation("ApointmentTickets");

                    b.Navigation("RegisterTickets");
                });
#pragma warning restore 612, 618
        }
    }
}
