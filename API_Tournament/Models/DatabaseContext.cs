using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_Tournament.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<GameGroup> GameGroups { get; set; } = null!;
        public virtual DbSet<GameProperty> GameProperties { get; set; } = null!;
        public virtual DbSet<GameType> GameTypes { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<TournamentRequest> TournamentRequests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.PkGameId)
                    .HasName("PK__game__FCA8F854777BCFC3");

                entity.ToTable("game");

                entity.Property(e => e.PkGameId).HasColumnName("pk_game_id");

                entity.Property(e => e.FkGameTypeId).HasColumnName("fk_game_type_id");

                entity.Property(e => e.FkTournamentId).HasColumnName("fk_tournament_id");

                entity.Property(e => e.GameTitle)
                    .HasMaxLength(255)
                    .HasColumnName("game_title");

                entity.Property(e => e.IsTemplate)
                    .HasColumnName("is_template")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PointsForFirst).HasColumnName("points_for_first");

                entity.Property(e => e.PointsForLast)
                    .HasColumnName("points_for_last")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PointsForScore).HasColumnName("points_for_score");

                entity.HasOne(d => d.FkGameType)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.FkGameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__fk_game_ty__04E4BC85");

                entity.HasOne(d => d.FkTournament)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.FkTournamentId)
                    .HasConstraintName("FK__game__fk_tournam__03F0984C");
            });

            modelBuilder.Entity<GameGroup>(entity =>
            {
                entity.HasKey(e => e.PkGroupId)
                    .HasName("PK__game_gro__978937F23265A7BE");

                entity.ToTable("game_group");

                entity.Property(e => e.PkGroupId).HasColumnName("pk_group_id");

                entity.Property(e => e.FkGameId).HasColumnName("fk_game_id");

                entity.Property(e => e.FkGroupParentId).HasColumnName("fk_group_parent_id");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .HasColumnName("group_name");

                entity.Property(e => e.IsFinished)
                    .HasColumnName("is_finished")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NextStagePlayerCount).HasColumnName("next_stage_player_count");

                entity.HasOne(d => d.FkGame)
                    .WithMany(p => p.GameGroups)
                    .HasForeignKey(d => d.FkGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_grou__fk_ga__01142BA1");

                entity.HasOne(d => d.FkGroupParent)
                    .WithMany(p => p.InverseFkGroupParent)
                    .HasForeignKey(d => d.FkGroupParentId)
                    .HasConstraintName("FK__game_grou__fk_gr__00200768");
            });

            modelBuilder.Entity<GameProperty>(entity =>
            {
                entity.HasKey(e => e.PkPropertyId)
                    .HasName("PK__game_pro__7960D8691E183E7D");

                entity.ToTable("game_properties");

                entity.Property(e => e.PkPropertyId).HasColumnName("pk_property_id");

                entity.Property(e => e.FkGameId).HasColumnName("fk_game_id");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(255)
                    .HasColumnName("property_name");

                entity.Property(e => e.PropertyType)
                    .HasMaxLength(255)
                    .HasColumnName("property_type");

                entity.Property(e => e.PropertyValue)
                    .HasMaxLength(255)
                    .HasColumnName("property_value");

                entity.HasOne(d => d.FkGame)
                    .WithMany(p => p.GameProperties)
                    .HasForeignKey(d => d.FkGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_prop__fk_ga__7F2BE32F");
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.HasKey(e => e.PkGameTypeId)
                    .HasName("PK__game_typ__D4AC80726DC74809");

                entity.ToTable("game_type");

                entity.Property(e => e.PkGameTypeId).HasColumnName("pk_game_type_id");

                entity.Property(e => e.GameTypeName)
                    .HasMaxLength(255)
                    .HasColumnName("game_type_name");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PkPlayerId)
                    .HasName("PK__players__049E3A4C96CC620D");

                entity.ToTable("players");

                entity.Property(e => e.PkPlayerId).HasColumnName("pk_player_id");

                entity.Property(e => e.FkTournamentId).HasColumnName("fk_tournament_id");

                entity.Property(e => e.FkUserId).HasColumnName("fk_user_id");

                entity.HasOne(d => d.FkTournament)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.FkTournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__players__fk_tour__7D439ABD");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__players__fk_user__7C4F7684");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => e.PkScoreId)
                    .HasName("PK__score__D99C6B7F6231078C");

                entity.ToTable("score");

                entity.Property(e => e.PkScoreId).HasColumnName("pk_score_id");

                entity.Property(e => e.FkGameGroupId).HasColumnName("fk_game_group_id");

                entity.Property(e => e.FkPlayerId).HasColumnName("fk_player_id");

                entity.Property(e => e.Score1)
                    .HasColumnName("score")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FkGameGroup)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.FkGameGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__score__fk_game_g__02084FDA");

                entity.HasOne(d => d.FkPlayer)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.FkPlayerId)
                    .HasConstraintName("FK__score__fk_player__02FC7413");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.PkTournamentId)
                    .HasName("PK__tourname__BEC753A5C7113E15");

                entity.ToTable("tournament");

                entity.HasIndex(e => e.Title, "UQ__tourname__E52A1BB34BE6E2B2")
                    .IsUnique();

                entity.Property(e => e.PkTournamentId).HasColumnName("pk_tournament_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.IsPrivate)
                    .HasColumnName("is_private")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartsAt)
                    .HasColumnType("datetime")
                    .HasColumnName("starts_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.TournamentReferee).HasColumnName("tournament_referee");

                entity.HasOne(d => d.TournamentRefereeNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.TournamentReferee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tournamen__tourn__7E37BEF6");
            });

            modelBuilder.Entity<TournamentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__tourname__18D3B90FB61F3544");

                entity.ToTable("tournament_request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.AdditionalNotes)
                    .HasMaxLength(255)
                    .HasColumnName("additional_notes");

                entity.Property(e => e.CompanySize).HasColumnName("company_size");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.RequesterName)
                    .HasMaxLength(255)
                    .HasColumnName("requester_name");

                entity.Property(e => e.TournamentEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("tournament_end");

                entity.Property(e => e.TournamentStart)
                    .HasColumnType("datetime")
                    .HasColumnName("tournament_start");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PkUserId)
                    .HasName("PK__users__2F41631384E0018F");

                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572C148FD22")
                    .IsUnique();

                entity.Property(e => e.PkUserId).HasColumnName("pk_user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsSuspended)
                    .HasColumnName("is_suspended")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
