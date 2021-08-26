using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudyBuddyTest.Models
{
    public partial class StudyDBContext : DbContext
    {
        public StudyDBContext()
        {
        }

        public StudyDBContext(DbContextOptions<StudyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<QandA> QandAs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=StudyDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.Qid, e.UserName })
                    .HasName("PK__Favorite__A62E6F8E5BDF7084");

                entity.Property(e => e.Qid).HasColumnName("QID");

                entity.Property(e => e.UserName).HasMaxLength(30);

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.Qid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__QID__3F466844");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__UserN__403A8C7D");
            });

            modelBuilder.Entity<QandA>(entity =>
            {
                entity.HasKey(e => e.Qid)
                    .HasName("PK__QandA__CAB1462B7A4B7065");

                entity.ToTable("QandA");

                entity.Property(e => e.Qid).HasColumnName("QId");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F28457CB1CFE70");

                entity.Property(e => e.UserName).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
