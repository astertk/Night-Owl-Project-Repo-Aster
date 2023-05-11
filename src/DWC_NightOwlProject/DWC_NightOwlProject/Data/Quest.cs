using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DWC_NightOwlProject.Data;

public partial class Quest
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UserID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [StringLength(450)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column("WorldID")]
    public int WorldId { get; set; }

    [StringLength(1000)]
    public string Prompt { get; set; } = null!;

    [StringLength(4000)]
    public string Completion { get; set; } = null!;
}
