using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using apilearning.Models;

namespace apilearning.MyDbContext {
    public partial class accountContext : DbContext {
        public accountContext() {
        }

        public accountContext(DbContextOptions<accountContext> options)
            : base(options) {
        }

        public virtual DbSet<AccountMovie> AccountMovies { get; set; } = null!;

        public virtual DbSet<MyAccount> MyAccounts { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //    if(!optionsBuilder.IsConfigured) {

        //        optionsBuilder.UseMySql("server=localhost;user=root;password=123456;database=account", ServerVersion.Parse("8.0.28-mysql"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AccountMovie>(entity => {
                entity.ToTable("account_movie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MovieContent)
                    .HasColumnName("movie_content")
                    .HasDefaultValueSql("_utf8mb3\\'\\'");

                entity.Property(e => e.MovieDate).HasColumnName("movie_date");

                entity.Property(e => e.MovieEvaluate)
                    .HasMaxLength(200)
                    .HasColumnName("movie_evaluate");

                entity.Property(e => e.MovieFilmName)
                    .HasMaxLength(100)
                    .HasColumnName("movie_film_name");

                entity.Property(e => e.MovieHeroName)
                    .HasMaxLength(100)
                    .HasColumnName("movie_hero_name");

                entity.Property(e => e.MovieImg)
                    .HasMaxLength(100)
                    .HasColumnName("movie_img");
            });


            modelBuilder.Entity<MyAccount>(entity => {
                entity.ToTable("my_account");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountDescription)
                    .HasMaxLength(100)
                    .HasColumnName("account_description");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .HasColumnName("account_name");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(50)
                    .HasColumnName("account_password");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("modify_date");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
