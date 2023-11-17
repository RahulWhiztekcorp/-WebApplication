using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models;

public partial class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {
    }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentTbl> DepartmentTbls { get; set; }

    public virtual DbSet<EmployeeTbl> EmployeeTbls { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer(Con);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentTbl>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Departme__C0365650F05D886B");

            entity.ToTable("Department_tbl");

            entity.Property(e => e.Did).HasColumnName("DId");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeTbl>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__Employee__C190176B374F153D");

            entity.ToTable("Employee_tbl");

            entity.Property(e => e.Eid).HasColumnName("EId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeeTbls)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee___Depar__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
