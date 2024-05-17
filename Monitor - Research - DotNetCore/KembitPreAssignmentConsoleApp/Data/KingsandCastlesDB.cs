using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class KingsandCastlesDB : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=KingsandCastlesDB;Trusted_Connection=True;");
    }

    public DbSet<King>? Kings { get; set; }
    public DbSet<Castle>? Castles { get; set; }
}

