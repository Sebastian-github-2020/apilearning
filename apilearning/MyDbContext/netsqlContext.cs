using System;
using System.Collections.Generic;
using apilearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace apilearning.MyDbContext
{
    public partial class netsqlContext : DbContext
    {
        public netsqlContext()
        {
        }

        public netsqlContext(DbContextOptions<netsqlContext> options)
            : base(options)
        {
           
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        //public IConfiguration Configuration { get; }=null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseMySql(Configuration.GetConnectionString("Default"), ServerVersion.Parse("8.0.29-mysql"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasComment("数据表模型");

                entity.Property(e => e.BornDate)
                    .HasColumnType("datetime")
                    .HasComment("出生日期");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasComment("姓名");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
