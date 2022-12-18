using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

[Table("regular_task")]
public partial class regular_task
{
    [Key]
    public int task_id { get; set; }

    public string type { get; set; } = null!;

    public int contact_id { get; set; }

    public string author { get; set; }

    public string executor { get; set; }

    public DateTime created { get; set; }

    public DateTime? finished { get; set; }

    [StringLength(10)]
    public string priority { get; set; } = null!;

    public DateTime? deadline { get; set; }

    [ForeignKey("contact_id")]
    [InverseProperty("regular_tasks")]
    public virtual contact_employee contact { get; set; } = null!;
}
