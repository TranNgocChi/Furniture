using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureApp.Models.VNMaps;

[Table("districts")]
[Index("ProvinceCode", Name = "idx_districts_province")]
[Index("AdministrativeUnitId", Name = "idx_districts_unit")]
public partial class District
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

    [Column("province_code")]
    [StringLength(20)]
    public string? ProvinceCode { get; set; }

    [Column("administrative_unit_id")]
    public int? AdministrativeUnitId { get; set; }

    [ForeignKey("AdministrativeUnitId")]
    [InverseProperty("Districts")]
    public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

    [ForeignKey("ProvinceCode")]
    [InverseProperty("Districts")]
    public virtual Province? ProvinceCodeNavigation { get; set; }

    [InverseProperty("DistrictCodeNavigation")]
    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
