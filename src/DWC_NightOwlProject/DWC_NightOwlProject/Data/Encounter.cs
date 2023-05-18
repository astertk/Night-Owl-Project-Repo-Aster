using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DWC_NightOwlProject.Data;

[Table("Encounter")]
public partial class Encounter
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UserID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [StringLength(450)]
    public string Biome { get; set; } = null!;

    [StringLength(450)]
    public string Type { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column("WorldID")]
    public int WorldId { get; set; }

    [StringLength(1000)]
    public string Prompt { get; set; } = null!;

    public string Completion { get; set; } = null!;
}
