using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

public partial class contact_employee
{
    [Key]
    public int contact_id { get; set; }

    [StringLength(40)]
    public string name { get; set; } = null!;

    [StringLength(40)]
    public string company { get; set; } = null!;

    [StringLength(11)]
    public string phone { get; set; } = null!;

    [ForeignKey("company")]
    [InverseProperty("contact_employees")]
    public virtual client companyNavigation { get; set; } = null!;

    [InverseProperty("contact")]
    public virtual ICollection<contract_tasks> contract_tasks { get; } = new List<contract_tasks>();

    [InverseProperty("contact")]
    public virtual ICollection<regular_task> regular_tasks { get; } = new List<regular_task>();
}
