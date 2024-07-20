using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Models.VNMaps;

public partial class VnMapContext : DbContext
{
	public VnMapContext()
	{
	}

	public VnMapContext(DbContextOptions<VnMapContext> options)
		: base(options)
	{
	}

	public virtual DbSet<AdministrativeRegion> AdministrativeRegions { get; set; }

	public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

	public virtual DbSet<District> Districts { get; set; }
	public virtual DbSet<Province> Provinces { get; set; }
	public virtual DbSet<Ward> Wards { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Data Source=VNHNDELROYVM;Database=FurnitureShop;User Id=sa;Password=Brightsun2003*;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder.Entity<AdministrativeRegion>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("administrative_regions_pkey");

			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		modelBuilder.Entity<AdministrativeUnit>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("administrative_units_pkey");

			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		modelBuilder.Entity<District>(entity =>
		{
			entity.HasKey(e => e.Code).HasName("districts_pkey");

			entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Districts).HasConstraintName("districts_administrative_unit_id_fkey");

			entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.Districts).HasConstraintName("districts_province_code_fkey");
		});

		modelBuilder.Entity<Province>(entity =>
		{
			entity.HasKey(e => e.Code).HasName("provinces_pkey");

			entity.HasOne(d => d.AdministrativeRegion).WithMany(p => p.Provinces).HasConstraintName("provinces_administrative_region_id_fkey");

			entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Provinces).HasConstraintName("provinces_administrative_unit_id_fkey");
		});

		modelBuilder.Entity<Ward>(entity =>
		{
			entity.HasKey(e => e.Code).HasName("wards_pkey");

			entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Wards).HasConstraintName("wards_administrative_unit_id_fkey");

			entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.Wards).HasConstraintName("wards_district_code_fkey");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
