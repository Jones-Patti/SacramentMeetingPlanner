using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SacramentMeetingPlanner.Data
{
    public  class SacramentMeetingPlannerContext : DbContext
    {
        public SacramentMeetingPlannerContext(DbContextOptions<SacramentMeetingPlannerContext> options) : base(options)
        {
        }

        public virtual DbSet<Bishopric> Bishopric { get; set; }
        public virtual DbSet<Hymn> Hymn { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Sacrament> Sacrament { get; set; }
        public virtual DbSet<Speaker> Speaker { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bishopric>(entity =>
            {
                entity.HasIndex(e => e.PeopleId)
                    .HasName("people_id");

                entity.Property(e => e.BishopricId)
                    .HasColumnName("bishopric_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PeopleId)
                    .HasColumnName("people_id")
                    .HasColumnType("int(6)");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Bishopric)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bishopric_ibfk_1");
            });

            modelBuilder.Entity<Hymn>(entity =>
            {
                entity.Property(e => e.HymnId)
                    .HasColumnName("hymn_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.HymnTitle)
                    .IsRequired()
                    .HasColumnName("hymn_title")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.Property(e => e.PeopleId)
                    .HasColumnName("people_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Sacrament>(entity =>
            {
                entity.HasIndex(e => e.ClosingHymn)
                    .HasName("closing_hymn");

                entity.HasIndex(e => e.ClosingPrayer)
                    .HasName("closing_prayer");

                entity.HasIndex(e => e.ConductingBishopric)
                    .HasName("conducting_bishopric");

                entity.HasIndex(e => e.IntermediateHymn)
                    .HasName("intermediate_hymn");

                entity.HasIndex(e => e.OpeningHymn)
                    .HasName("opening_hymn");

                entity.HasIndex(e => e.OpeningPrayer)
                    .HasName("opening_prayer");

                entity.HasIndex(e => e.SacramentHymn)
                    .HasName("sacrament_hymn");

                entity.Property(e => e.SacramentId)
                    .HasColumnName("sacrament_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.ClosingHymn)
                    .HasColumnName("closing_hymn")
                    .HasColumnType("int(6)");

                entity.Property(e => e.ClosingPrayer)
                    .HasColumnName("closing_prayer")
                    .HasColumnType("int(6)");

                entity.Property(e => e.ConductingBishopric)
                    .HasColumnName("conducting_bishopric")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IntermediateHymn)
                    .HasColumnName("intermediate_hymn")
                    .HasColumnType("int(6)");

                entity.Property(e => e.OpeningHymn)
                    .HasColumnName("opening_hymn")
                    .HasColumnType("int(6)");

                entity.Property(e => e.OpeningPrayer)
                    .HasColumnName("opening_prayer")
                    .HasColumnType("int(6)");

                entity.Property(e => e.SacramentDate)
                    .HasColumnName("sacrament_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SacramentHymn)
                    .HasColumnName("sacrament_hymn")
                    .HasColumnType("int(6)");

                entity.HasOne(d => d.ClosingHymnNavigation)
                    .WithMany(p => p.SacramentClosingHymnNavigation)
                    .HasForeignKey(d => d.ClosingHymn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_7");

                entity.HasOne(d => d.ClosingPrayerNavigation)
                    .WithMany(p => p.SacramentClosingPrayerNavigation)
                    .HasForeignKey(d => d.ClosingPrayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_3");

                entity.HasOne(d => d.ConductingBishopricNavigation)
                    .WithMany(p => p.Sacrament)
                    .HasForeignKey(d => d.ConductingBishopric)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_2");

                entity.HasOne(d => d.IntermediateHymnNavigation)
                    .WithMany(p => p.SacramentIntermediateHymnNavigation)
                    .HasForeignKey(d => d.IntermediateHymn)
                    .HasConstraintName("Sacrament_ibfk_6");

                entity.HasOne(d => d.OpeningHymnNavigation)
                    .WithMany(p => p.SacramentOpeningHymnNavigation)
                    .HasForeignKey(d => d.OpeningHymn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_4");

                entity.HasOne(d => d.OpeningPrayerNavigation)
                    .WithMany(p => p.SacramentOpeningPrayerNavigation)
                    .HasForeignKey(d => d.OpeningPrayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_1");

                entity.HasOne(d => d.SacramentHymnNavigation)
                    .WithMany(p => p.SacramentSacramentHymnNavigation)
                    .HasForeignKey(d => d.SacramentHymn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sacrament_ibfk_5");
            });

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.HasIndex(e => e.PeopleId)
                    .HasName("people_id");

                entity.HasIndex(e => e.SacramentId)
                    .HasName("sacrament_id");

                entity.HasIndex(e => e.TopicId)
                    .HasName("topic_id");

                entity.Property(e => e.SpeakerId)
                    .HasColumnName("speaker_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.PeopleId)
                    .HasColumnName("people_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.SacramentId)
                    .HasColumnName("sacrament_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.TopicId)
                    .HasColumnName("topic_id")
                    .HasColumnType("int(6)");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Speaker_ibfk_1");

                entity.HasOne(d => d.Sacrament)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.SacramentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Speaker_ibfk_2");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Speaker_ibfk_3");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId)
                    .HasColumnName("topic_id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.TopicTitle)
                    .IsRequired()
                    .HasColumnName("topic_title")
                    .HasMaxLength(60);
            });
        }
    }
}
