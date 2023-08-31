using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentsCoreApi.Domain;
using PaymentsCoreApi.Domain.Entities;

namespace PaymentsCoreApi.Data.Contexts
{
    //generate a class for a database context class for mysql entity framework
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customers>()
                .HasIndex(u => u.CustomerId)
                .IsUnique();

            builder.Entity<Account>()
                .HasIndex(u => u.AccountNumber)
                .IsUnique();

            builder.Entity<Agents>()
                .HasIndex(u => u.AgentId)
                .IsUnique();

            builder.Entity<Country>()
                .HasIndex(u => u.CountryCode)
                .IsUnique();

            builder.Entity<UserLogins>()
               .HasIndex(u => u.Username)
               .IsUnique();

            builder.Entity<Products>()
              .HasIndex(u => u.ProductCode)
              .IsUnique();

            builder.Entity<Vendors>()
             .HasIndex(u => u.VendorCode)
             .IsUnique();
        }
        public DbSet<Vendors> Vendors { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<SignUpRequest> SignUpRequest { get; set; }
        public DbSet<UserLogins> UserLogins { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<TransactionHeader> TransactionHeader { get; set; }
        public DbSet<GeneralLedger> GeneralLedger { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<PasswordResetRequests> PasswordResetRequests { get; set; }
        public DbSet<AgentSignUpRequest> AgentSignUpRequests { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ThirdPartyDeposits> ThirdPartyDeposits{get;set;}
        public DbSet<Agents> Agents { get; set; }
        public DbSet<UserDocuments> UserDocuments { get; set; }


    }

}


