﻿using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public partial class MvvmDemoDbContext: DbContext
    {
          public  DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("server=.\\SQLExpress;user=sa;password=sa@123;database=MvvmDemoDb;Encrypt = False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>(entity =>
            //{
            //    entity.HasOne("Employee");
            //    entity.Property(e => e.Id).ValueGeneratedNever();
            //    entity.Property(e => e.Age).HasMaxLength(50);
            //    entity.Property(e => e.Name).HasMaxLength(50);
            //});

            // OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
