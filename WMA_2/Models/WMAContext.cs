using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WMA_2.Models;

namespace WMA_2.Models
{
    public partial class WMAContext : DbContext
    {
        public virtual DbSet<ContactFormat> ContactFormats { get; set; }
        public virtual DbSet<ContactMethod> ContactMethods { get; set; }
        public virtual DbSet<Audit> WmaAudit { get; set; }
        public virtual DbSet<WMA_Class> WmaClasses { get; set; }
        public virtual DbSet<WMA_Constant> WmaConstants { get; set; }
        public virtual DbSet<Contact> WmaContacts { get; set; }
        public virtual DbSet<WMA_Role> WmaRoles { get; set; }
        public virtual DbSet<ScheduleException> WmaScheduleException { get; set; }
        public virtual DbSet<WMA_User> WmaUser { get; set; }
        public virtual DbSet<UserClass> WmaUserClass { get; set; }
        public virtual DbSet<UserRoles> WmaUserRoles { get; set; }
        public virtual DbSet<Belt_Constraint> WmaBeltLevels { get; set; }
        public virtual DbSet<Age_Constraint> WmaAgeLevels { get; set; }
        public virtual DbSet<Class_Constraint> WmaClass_Constraints { get; set; }
        public virtual DbSet<Class_Times> WmaClass_Time { get; set; }
        public virtual DbSet<ClassTimes> WmaClassTime { get; set; }
        public virtual DbSet<WMA_Constant_Category> WmaConstant_Category { get; set; }
        public virtual DbSet<WMA_Location> WmaLocation { get; set; }

        public WMAContext(DbContextOptions<WMAContext> options) : base(options) {
        }

        public WMAContext()
        {
        }

        /* removed so that DI can be done in the startup.cs 
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
   #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
   optionsBuilder.UseSqlServer(@"dbconnection");
}*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactFormat>(entity =>
            {
                entity.HasKey(e => e.ContactFormatTypeId)
                    .HasName("PK__ContactF__703ED1E3B8736C8F");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FormatingDescription)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ContactMethod>(entity =>
            {
                entity.HasKey(e => e.ContactMethodId)
                    .HasName("PK__ContactM__0BC85A5B7EC3CF6E");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.FormatType).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(e => e.AuditId)
                    .HasName("PK__wmaAudit__A17F23981D114CE0");

                entity.ToTable("wmaAudit");

                entity.Property(e => e.Changes).HasColumnType("varchar(50)");

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LastModifiedDate).HasColumnType("date");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<WMA_Class>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK__wmaClass__CB1927C0191B8D75");

                entity.ToTable("wmaClasses");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(1024)");
                // entity.HasMany(h => (ICollection<Class_Constraint>)h.Contraints);
            });

            modelBuilder.Entity<WMA_Constant>(entity =>
            {
                entity.HasKey(e => e.ConstantId)
                    .HasName("PK__wmaConst__66315FDFEB5CB0E2");

                entity.ToTable("wmaConstants");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__wmaConta__5C66259BEAB0FDB9");

                entity.ToTable("wmaContacts");

                entity.Property(e => e.Description).HasColumnType("varchar(1024)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<WMA_Role>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__wmaRoles__8AFACE1AA3E841B6");

                entity.ToTable("wmaRoles");

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ScheduleException>(entity =>
            {
                entity.HasKey(e => e.ScheduleExceptionId)
                    .HasName("PK__wmaSched__AA7D417043B4C310");

                entity.ToTable("wmaScheduleException");

                entity.Property(e => e.ClassDate).HasColumnType("date");
            });

            modelBuilder.Entity<WMA_User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__wmaUser__1788CC4C728B51BB");

                entity.ToTable("wmaUser");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<UserClass>(entity =>
            {
                entity.HasKey(e => e.UserClassId)
                    .HasName("PK__wmaUserC__1518702F0ED83389");

                entity.ToTable("wmaUserClass");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK__wmaUserR__3D978A35DD60464F");

                entity.ToTable("wmaUserRoles");
            });
        }

        /* removed so that DI can be done in the startup.cs 
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
   #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
   optionsBuilder.UseSqlServer(@"dbconnection");
}*/

        public DbSet<WMA_2.Models.ClassTimes> ClassTimes { get; set; }
    }
}