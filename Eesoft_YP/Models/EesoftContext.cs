using Microsoft.EntityFrameworkCore;

namespace Eesoft_YP.Models;

public partial class EesoftContext : DbContext
{
    public EesoftContext()
    {
    }

    public EesoftContext(DbContextOptions<EesoftContext> options) : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Estate> Estates { get; set; }

    public virtual DbSet<EstateDemand> EstateDemands { get; set; }

    public virtual DbSet<EstateType> EstateTypes { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI;Database=Eesoft;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("Agent");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DemandId).HasColumnName("Demand_ID");
            entity.Property(e => e.SupplyId).HasColumnName("Supply_ID");

            entity.HasOne(d => d.Demand).WithMany(p => p.Deals)
                .HasForeignKey(d => d.DemandId)
                .HasConstraintName("FK_Deals_Estate-Demands");

            entity.HasOne(d => d.Supply).WithMany(p => p.Deals)
                .HasForeignKey(d => d.SupplyId)
                .HasConstraintName("FK_Deals_Supplies");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Estate>(entity =>
        {
            entity.ToTable("Estate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressCity).HasColumnName("Address_City");
            entity.Property(e => e.AddressHouse).HasColumnName("Address_House");
            entity.Property(e => e.AddressNumber).HasColumnName("Address_Number");
            entity.Property(e => e.CoordinateLatitude).HasColumnName("Coordinate_latitude");
            entity.Property(e => e.CoordinateLongitude).HasColumnName("Coordinate_longitude");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.EstateType).HasColumnName("Estate_Type");
            entity.Property(e => e.TotalArea).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.District).WithMany(p => p.Estates)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Estate_Districts");

            entity.HasOne(d => d.EstateTypeNavigation).WithMany(p => p.Estates)
                .HasForeignKey(d => d.EstateType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate_Estate-Type");
        });

        modelBuilder.Entity<EstateDemand>(entity =>
        {
            entity.ToTable("Estate-Demands");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentId).HasColumnName("Agent_ID");
            entity.Property(e => e.ClientId).HasColumnName("Client_ID");
            entity.Property(e => e.EstateType).HasColumnName("Estate_Type");
            entity.Property(e => e.MaxArea).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MaxPrice).HasColumnType("money");
            entity.Property(e => e.MinArea).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MinPrice).HasColumnType("money");

            entity.HasOne(d => d.Agent).WithMany(p => p.EstateDemands)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate-Demands_Agent");

            entity.HasOne(d => d.Client).WithMany(p => p.EstateDemands)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate-Demands_Client");

            entity.HasOne(d => d.EstateTypeNavigation).WithMany(p => p.EstateDemands)
                .HasForeignKey(d => d.EstateType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate-Demands_Estate-Type");
        });

        modelBuilder.Entity<EstateType>(entity =>
        {
            entity.ToTable("Estate-Type");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentId).HasColumnName("Agent_ID");
            entity.Property(e => e.ClientId).HasColumnName("Client_ID");
            entity.Property(e => e.EstateId).HasColumnName("Estate_ID");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Agent).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Agent");

            entity.HasOne(d => d.Client).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Client");

            entity.HasOne(d => d.Estate).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.EstateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Estate-Demands");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
