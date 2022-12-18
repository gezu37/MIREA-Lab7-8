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

namespace company1.Pages.regular_tasks
{
    public class EditModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public EditModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public regular_task regular_task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.regular_tasks == null)
            {
                return NotFound();
            }

            var regular_task_ =  await _context.regular_tasks.FirstOrDefaultAsync(m => m.task_id == id);
            if (regular_task_ == null)
            {
                return NotFound();
            }
            regular_task = regular_task_;
           ViewData["contact_id"] = new SelectList(_context.contact_employees, "contact_id", "company");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
               // return Page();
            }
            regular_task.created = regular_task.created.ToUniversalTime();
            regular_task.finished = regular_task.finished.GetValueOrDefault().ToUniversalTime();
            regular_task.deadline = regular_task.deadline.GetValueOrDefault().ToUniversalTime();
            _context.Attach(regular_task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!regular_taskExists(regular_task.task_id))
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

        private bool regular_taskExists(int id)
        {
          return _context.regular_tasks.Any(e => e.task_id == id);
        }
    }
}
