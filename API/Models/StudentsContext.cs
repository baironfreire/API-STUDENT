using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class StudentsContext : DbContext
{
    public StudentsContext()
    {
    }

    public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Qualification> Qualifications { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.QualificationsId).HasName("PK__qualific__8EA9F583F9FB6523");
            entity.ToTable("qualifications");
            entity.Property(e => e.QualificationsId).HasColumnName("qualificationId");
            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.QualificationName).HasColumnName("qualificationName");
            entity.HasOne(e => e.Student)
            .WithMany(e => e.Qualifications)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__students__4D11D63C292884B7");
            entity.ToTable("students");
            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.Address).HasMaxLength(100).HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.HasMany(e => e.Qualifications).WithOne(e => e.Student).OnDelete(DeleteBehavior.Cascade);
        });
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
