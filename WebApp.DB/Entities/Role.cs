using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DB.Entities;

[Table("Role", Schema = "Master")]
public partial class Role
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string RoleId { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
