using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DWC_NightOwlProject.Data;

[Table("Template")]
public partial class Template
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [StringLength(1000)]
    public string Body { get; set; } = null!;

    [StringLength(250)]
    public string Type { get; set; } = null!;

    [InverseProperty("Template")]
    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
