using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

[Index("address", Name = "clients_address")]
[Index("company", Name = "clients_name")]
public partial class client
{
    [Key]
    [StringLength(40)]
    public string company { get; set; } = null!;

    [StringLength(11)]
    public string? phone { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    [InverseProperty("companyNavigation")]
    public virtual ICollection<contact_employee> contact_employees { get; } = new List<contact_employee>();
}
