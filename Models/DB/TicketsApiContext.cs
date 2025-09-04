using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketsApi.Models.DB;

public partial class TicketsApiContext : DbContext
{
    public TicketsApiContext()
    {
    }

    public TicketsApiContext(DbContextOptions<TicketsApiContext> options) : base(options)
    {
    }

    public virtual DbSet<TicketsApi.Models.Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=TicketsApi; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketsApi.Models.Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket");

            entity.ToTable("Ticket");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
           .HasConversion(
               v => v.ToString(),
               v => (TicketStatus)Enum.Parse(typeof(TicketStatus), v))
           .HasMaxLength(150)
           .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.User)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
