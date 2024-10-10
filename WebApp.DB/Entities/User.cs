using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DB.Entities;

[Table("User", Schema = "Master")]
public partial class User
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string UserAccountId { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string RoleId { get; set; } = null!;

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
