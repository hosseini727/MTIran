using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { 
        }
        public  DbSet<Book> Books { get; set; }
        public  DbSet<Member> Members { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<LoanBook>()
                .HasOne(l => l.Member)
                .WithMany(m => m.LoanBooks)
                .HasForeignKey(l => l.MemberId);

            modelBuilder.Entity<LoanBook>()
                .HasOne(l => l.Book)
                .WithMany(b => b.LoanBooks)
                .HasForeignKey(l => l.BookId);
        }
    }

}

