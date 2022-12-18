using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyModels.Data;
using CompanyModels.Models;

namespace company1.Pages.contract_tasks
{
    public class EditModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public EditModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CompanyModels.Models.contract_tasks contract_task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.contract_tasks == null)
            {
                return NotFound();
            }

            var contract_task_ =  await _context.contract_tasks.FirstOrDefaultAsync(m => m.task_id == id);
            if (contract_task_ == null)
            {
                return NotFound();
            }
            contract_task = contract_task_;
           ViewData["contact_id"] = new SelectList(_context.contact_employees, "contact_id", "name");
           ViewData["contract_id"] = new SelectList(_context.contracts, "contract_id", "contract_text");
           ViewData["equipment_id"] = new SelectList(_context.equipment, "equipment_id", "name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            contract_task.created = contract_task.created.ToUniversalTime();
            contract_task.finished = contract_task.finished.GetValueOrDefault().ToUniversalTime();
            contract_task.deadline = contract_task.deadline.GetValueOrDefault().ToUniversalTime();
            _context.Attach(contract_task).State = EntityState.Modified;

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
