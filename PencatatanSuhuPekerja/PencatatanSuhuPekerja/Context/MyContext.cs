using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Context
{
    public class MyContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }    
        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Employee <=> User relationship
            modelBuilder.Entity<Employee>()
                .HasOne<User>(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<User>(u => u.Id);

            //User <=> Role relationship
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
