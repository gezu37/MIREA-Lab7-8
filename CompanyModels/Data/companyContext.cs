using System;
using System.Collections.Generic;
using CompanyModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace CompanyModels.Data;

public partial class companyContext : DbContext
{
    public companyContext()
    {
    }

    public companyContext(DbContextOptions<companyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<client> clients { get; set; }

    public virtual DbSet<contact_employee> contact_employees { get; set; }

    public virtual DbSet<contract> contracts { get; set; }

    public virtual DbSet<contract_tasks> contract_tasks { get; set; }

    public virtual DbSet<equipment> equipment { get; set; }

    public virtual DbSet<potent_client> potent_clients { get; set; }

    public virtual DbSet<regular_task> regular_tasks { get; set; }


    private readonly HttpContext _httpContext;

    public companyContext(DbContextOptions<companyContext> options, IHttpContextAccessor httpContextAccessor = null)
        : base(options)
    {
        _httpContext = httpContextAccessor?.HttpContext;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var claim_password = _httpContext.User.Claims.First(c => c.Type == "password");
        var password = claim_password.Value;
        var claim_login = _httpContext.User.Claims.First(c => c.Type == "login");
        var login = claim_login.Value;
        optionsBuilder.UseNpgsql($"Host = 127.0.0.1:5432;Database=company1;Username={login};Password={password}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<client>(entity =>
        {
            entity.HasKey(e => e.company).HasName("clients_pkey");
        });

        modelBuilder.Entity<contact_employee>(entity =>
        {
            entity.HasKey(e => e.contact_id).HasName("contact_employees_pkey");

            entity.HasOne(d => d.companyNavigation).WithMany(p => p.contact_employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_employees_company_fkey");
        });

        modelBuilder.Entity<contract>(entity =>
        {
            entity.HasKey(e => e.contract_id).HasName("contracts_pkey");
        });

        modelBuilder.Entity<contract_tasks>(entity =>
        {
            entity.HasKey(e => e.task_id).HasName("contract_tasks_pkey");

            entity.HasOne(d => d.contact).WithMany(p => p.contract_tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_tasks_contact_id_fkey");

            entity.HasOne(d => d.contract).WithMany(p => p.contract_tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_tasks_contract_id_fkey");

            entity.HasOne(d => d.equipment).WithMany(p => p.contract_tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_tasks_equipment_id_fkey");
        });

        modelBuilder.Entity<equipment>(entity =>
        {
            entity.HasKey(e => e.equipment_id).HasName("equipment_pkey");
        });

        modelBuilder.Entity<potent_client>(entity =>
        {
            entity.HasKey(e => e.company).HasName("potent_clients_pkey");
        });

        modelBuilder.Entity<regular_task>(entity =>
        {
            entity.HasKey(e => e.task_id).HasName("regular_task_pkey");

            entity.HasOne(d => d.contact).WithMany(p => p.regular_tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("regular_task_contact_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
