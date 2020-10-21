using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DockerSample.Models
{
    public partial class orgContext : DbContext
    {
        public orgContext()
        {
        }

        public orgContext(DbContextOptions<orgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TblDept> TblDept { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=org;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Deptid)
                    .HasName("PK__Departme__01499C76D094BDA5");

                entity.Property(e => e.Deptname).HasMaxLength(20);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PK__Employee__D9509F6D1C9F14F4");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.Deptid).HasColumnName("deptid");

                entity.Property(e => e.Designation)
                    .HasColumnName("designation")
                    .HasMaxLength(50);

                entity.Property(e => e.Doj)
                    .HasColumnName("doj")
                    .HasColumnType("date");

                entity.Property(e => e.Ename)
                    .HasColumnName("ename")
                    .HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Deptid)
                    .HasConstraintName("FK__Employee__deptid__38996AB5");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK_dbo.Products");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Dop)
                    .HasColumnName("dop")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pname).HasColumnName("pname");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Qty).HasColumnName("qty");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId)
                    .HasColumnName("tagID")
                    .HasMaxLength(20);

                entity.Property(e => e.TagDesc)
                    .HasColumnName("tagDesc")
                    .HasMaxLength(50);

                entity.Property(e => e.TagName)
                    .HasColumnName("tagName")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TblDept>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("tblDept");

                entity.Property(e => e.Did).HasColumnName("did");

                entity.Property(e => e.Dname)
                    .HasColumnName("dname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
