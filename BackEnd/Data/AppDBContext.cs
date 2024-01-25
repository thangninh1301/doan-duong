using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using BackEnd.Helper.Seeding;

namespace BackEnd.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            this._options = options;
        }

        public DbSet<Result> Results { get; set; }



        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApointmentTicket> ApointmentTickets { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        public DbSet<RegisterTicket> RegisterTickets { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<ResultDetail> ResultDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ResultDetail>().HasKey(a => new { a.IdResult, a.IdDoctorTest });
           /* modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(ub => ub.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(ub => ub.UserId);*/
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ub => new { ub.UserId, ub.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(ub => ub.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(ub => ub.UserId);
            modelBuilder.Entity<IdentityUserClaim<string>>();
            modelBuilder.Entity<IdentityRoleClaim<string>>();

            modelBuilder.Entity<ApointmentTicket>()
               .HasOne(p => p.registerticket)
               .WithOne(cd => cd.apointmentTicket)
               .HasForeignKey<ApointmentTicket>(p => p.IdRegisterTicket);

            modelBuilder.Entity<TimeSlot>()
            .HasMany(s => s.ApointmentTickets)
            .WithOne(g => g.TimeSlot)
            .HasForeignKey(g => g.IdTimeMeet);
            modelBuilder.Entity<TimeSlot>()
              .HasMany(s => s.RegisterTickets)
              .WithOne(c => c.Timeslot)
              .HasForeignKey(c => c.IdTimeMeet);
            modelBuilder.Entity<ApplicationUser>()
             .HasMany(s => s.registerTickets)
             .WithOne(c => c.User)
             .HasForeignKey(c => c.IdPatient);
           /* modelBuilder.Entity<ResultDetail>()
                .HasKey(ub => new { ub.IdResult, ub.IdTest });*/
            modelBuilder.Entity<ApplicationUser>()
             .HasMany(s => s.ApointmentTickets)
             .WithOne(c => c.Doctor)
             .HasForeignKey(c => c.IdDoctor);
            modelBuilder.Entity<Department>()
             .HasMany(s => s.applicationUsers)
             .WithOne(c => c.department)
             .HasForeignKey(c => c.IdDepartment);

            modelBuilder.Entity<Result>()
           .HasOne<ApointmentTicket>(rs => rs.ApointmentTicket)
           .WithOne(ap => ap.Result)
           .HasForeignKey<Result>(rs => rs.IdApointmentTicket);
            modelBuilder.Entity<Result>()
            .HasMany(r => r.ResultDetails2)
            .WithOne(rd => rd.Result)
            .HasForeignKey(rd => rd.IdResult);
            modelBuilder.Entity<ResultDetail>()
            .HasOne(rd => rd.DoctorTest)
            .WithMany(t => t.ResultDetails1)
            .HasForeignKey(rd => rd.IdDoctorTest);
            modelBuilder.Entity<Test>()
                .HasMany(t => t.DoctorTests)
                .WithOne(dt => dt.Test)
                .HasForeignKey(dt => dt.IdTest);



            //seeding

            

            modelBuilder.Entity<TimeSlot>().HasData(
               new() { Id = 1, Deleted = false, Decription = "6h-9h" ,Max = 100},
               new() { Id = 2, Deleted = false, Decription = "9h-12h", Max = 100 },
               new() { Id = 3, Deleted = false, Decription = "13h-15h", Max = 100 },
               new() { Id = 4, Deleted = false, Decription = "15h-17h", Max = 100 }
            );

            modelBuilder.Entity<Department>().HasData(
               new() { Id = 1, Deleted = false, Decription = "Cột Sống", Name ="Cột Sống" },
               new() { Id = 2, Deleted = false, Decription = "Nội Tiết", Name = "Nội Tiết" },
               new() { Id = 3, Deleted = false, Decription = "Mắt", Name = "Mắt" },
               new() { Id = 4, Deleted = false, Decription = "Da Liễu", Name = "Da Liễu" }
            );
            ApplicationDbInitializer.SeedUsers(modelBuilder);
        }
    }

}
