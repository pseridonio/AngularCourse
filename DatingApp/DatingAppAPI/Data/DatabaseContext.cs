using DatingAppAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext([NotNullAttribute] DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.CreateUserTable(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateUserTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>(userEntity =>
            {
                userEntity.ToTable("TB_USERS");
                
                userEntity
                    .HasKey(User => User.UserID)
                    .HasName("PK_USERS");

                userEntity
                    .Property(user => user.UserID)
                    .HasColumnName("UserID")
                    .HasColumnType("Integer")
                    .IsRequired();

                userEntity
                    .Property(user => user.Name)
                    .HasColumnName("Name")
                    .HasColumnType("VARCHAR(250)")
                    .IsRequired();

                userEntity
                    .Property(user => user.PasswordSalt)
                    .HasColumnName("PASSWORD_SALT")
                    .HasColumnType("BLOB")
                    .IsRequired();

                userEntity
                    .Property(user => user.PasswordHash)
                    .HasColumnName("PASSWORD_HASH")
                    .HasColumnType("BLOB")
                    .IsRequired();
            });
        }
    }
}
