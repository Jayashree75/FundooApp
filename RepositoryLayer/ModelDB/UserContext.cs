using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.ModelDB
{
  public class UserContext : DbContext
  {
    public UserContext(DbContextOptions options)
           : base(options)
    {
    }

    public static object UserDetails { get; internal set; }
    public DbSet<UserDetails> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserDetails>()
        .HasIndex(user => user.Email)
        .IsUnique();

    }
  }
}
