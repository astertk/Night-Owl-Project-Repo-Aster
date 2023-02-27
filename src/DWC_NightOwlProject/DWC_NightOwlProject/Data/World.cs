using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DWC_NightOwlProject.Data;

[Table("World")]
public partial class World
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column("UserID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;
    
    [Column("NAME")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [InverseProperty("World")]
    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
