using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyModels.Data;
using CompanyModels.Models;
using Microsoft.EntityFrameworkCore;

namespace company1.Pages.contract_tasks
{
    public class CreateModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public CreateModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["contact_id"] = new SelectList(_context.contact_employees, "contact_id", "name");
        ViewData["contract_id"] = new SelectList(_context.contracts, "contract_id", "contract_text");
        ViewData["equipment_id"] = new SelectList(_context.equipment, "equipment_id", "name");
            return Page();
        }

        [BindProperty]
        public CompanyModels.Models.contract_tasks contract_task { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            contract_task.created = contract_task.created.ToUniversalTime();
            contract_task.deadline = contract_task.deadline.GetValueOrDefault().ToUniversalTime();
            _context.contract_tasks.Add(contract_task);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!contract_taskExists(contract_task.task_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
         
        }

        private bool contract_taskExists(int id)
        {
            return _context.contract_tasks.Any(e => e.task_id == id);
        }
    }      
}
    

