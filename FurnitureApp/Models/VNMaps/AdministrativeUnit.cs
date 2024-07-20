using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Models.VNMaps;

[Table("administrative_units")]
public partial class AdministrativeUnit
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("full_name")]
    [StringLength(255)]
    public string? FullName { get; set; }

    [Column("full_name_en")]
    [StringLength(255)]
    public string? FullNameEn { get; set; }

    [Column("short_name")]
    [StringLength(255)]
    public string? ShortName { get; set; }

    [Column("short_name_en")]
    [StringLength(255)]
    public string? ShortNameEn { get; set; }

    [Column("code_name")]
    [StringLength(255)]
    public string? CodeName { get; set; }

    [Column("code_name_en")]
    [StringLength(255)]
    public string? CodeNameEn { get; set; }

    [InverseProperty("AdministrativeUnit")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [InverseProperty("AdministrativeUnit")]
    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();

    [InverseProperty("AdministrativeUnit")]
    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
