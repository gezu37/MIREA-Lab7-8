using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyModels.Models;

public partial class potent_client
{
    [Key]
    [StringLength(40)]
    public string company { get; set; } = null!;

    [StringLength(11)]
    public string? phone { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }
}
