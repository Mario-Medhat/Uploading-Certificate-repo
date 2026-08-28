using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uploading_Certificate.Models;

namespace Uploading_Certificate.Data
{
    public class UcDbContext : DbContext
    {
        public UcDbContext(DbContextOptions<UcDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Certificate> Certificates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
