using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

public partial class contract_tasks
{
    [Key]
    public int task_id { get; set; }

    public string type { get; set; } = null!;

    public string author { get; set; }

    public string executor { get; set; }

    public int contact_id { get; set; }

    public DateTime created { get; set; }

    public DateTime? finished { get; set; }

    [StringLength(10)]
    public string priority { get; set; } = null!;

    public int contract_id { get; set; }

    public int equipment_id { get; set; }

    public DateTime? deadline { get; set; }

    [ForeignKey("contact_id")]
    [InverseProperty("contract_tasks")]
    public virtual contact_employee contact { get; set; } = null!;

    [ForeignKey("contract_id")]
    [InverseProperty("contract_tasks")]
    public virtual contract contract { get; set; } = null!;

    [ForeignKey("equipment_id")]
    [InverseProperty("contract_tasks")]
    public virtual equipment equipment { get; set; } = null!;
}
