using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

public partial class equipment
{
    [Key]
    public int equipment_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    public string? description { get; set; }

    [InverseProperty("equipment")]
    public virtual ICollection<contract_tasks> contract_tasks { get; } = new List<contract_tasks>();
}
