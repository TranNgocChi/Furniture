using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Models.VNMaps;

[Table("provinces")]
[Index("AdministrativeRegionId", Name = "idx_provinces_region")]
[Index("AdministrativeUnitId", Name = "idx_provinces_unit")]
public partial class Province
{
    [Key]
    [Column("code")]
    [StringLength(20)]
    public string Code { get; set; } = null!;

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("name_en")]
    [StringLength(255)]
    public string? NameEn { get; set; }

    [Column("full_name")]
    [StringLength(255)]
    public string FullName { get; set; } = null!;

    [Column("full_name_en")]
    [StringLength(255)]
    public string? FullNameEn { get; set; }

    [Column("code_name")]
    [StringLength(255)]
    public string? CodeName { get; set; }

    [Column("administrative_unit_id")]
    public int? AdministrativeUnitId { get; set; }

    [Column("administrative_region_id")]
    public int? AdministrativeRegionId { get; set; }

    [ForeignKey("AdministrativeRegionId")]
    [InverseProperty("Provinces")]
    public virtual AdministrativeRegion? AdministrativeRegion { get; set; }

    [ForeignKey("AdministrativeUnitId")]
    [InverseProperty("Provinces")]
    public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

    [InverseProperty("ProvinceCodeNavigation")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
