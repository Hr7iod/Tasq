using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TasqConfiguration());

            /*modelBuilder.Entity<Tasq>()
                .HasOne(t => t.Parent)
                .WithMany(t => t.Children)
                .OnDelete(DeleteBehavior.Cascade);*/
        }

        public DbSet<Tasq> Tasqs { get; set; }
        
    }
}
