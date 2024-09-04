using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureApp.Models.VNMaps;

[Table("wards")]
[Index("DistrictCode", Name = "idx_wards_district")]
[Index("AdministrativeUnitId", Name = "idx_wards_unit")]
public partial class Ward
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
    public string? FullName { get; set; }

    [Column("full_name_en")]
    [StringLength(255)]
    public string? FullNameEn { get; set; }

    [Column("code_name")]
    [StringLength(255)]
    public string? CodeName { get; set; }

    [Column("district_code")]
    [StringLength(20)]
    public string? DistrictCode { get; set; }

    [Column("administrative_unit_id")]
    public int? AdministrativeUnitId { get; set; }

    [ForeignKey("AdministrativeUnitId")]
    [InverseProperty("Wards")]
    public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

    [ForeignKey("DistrictCode")]
    [InverseProperty("Wards")]
    public virtual District? DistrictCodeNavigation { get; set; }
}
