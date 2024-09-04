using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureApp.Models.VNMaps;

[Table("administrative_regions")]
public partial class AdministrativeRegion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("name_en")]
    [StringLength(255)]
    public string NameEn { get; set; } = null!;

    [Column("code_name")]
    [StringLength(255)]
    public string? CodeName { get; set; }

    [Column("code_name_en")]
    [StringLength(255)]
    public string? CodeNameEn { get; set; }

    [InverseProperty("AdministrativeRegion")]
    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
