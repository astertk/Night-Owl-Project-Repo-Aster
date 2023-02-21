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
    public String UserId { get; set; }

    [InverseProperty("World")]
    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
