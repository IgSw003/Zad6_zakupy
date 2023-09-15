using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zad6_zakupy;
using Zad6_zakupy.Models;

namespace Zad6_zakupy.Data;

public partial class Zad6_zakupyContext : DbContext
{
    public Zad6_zakupyContext()
    {
    }

    public Zad6_zakupyContext(DbContextOptions<Zad6_zakupyContext> options)
        : base(options)
    {
    }

    public DbSet<Purchase> Purchase { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
