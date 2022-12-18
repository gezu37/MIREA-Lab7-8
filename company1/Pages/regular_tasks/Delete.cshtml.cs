using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyModels.Data;
using CompanyModels.Models;

namespace company1.Pages.regular_tasks
{
    public class DeleteModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public DeleteModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        [BindProperty]
      public regular_task regular_task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.regular_tasks == null)
            {
                return NotFound();
            }

            var regular_task_ = await _context.regular_tasks.FirstOrDefaultAsync(m => m.task_id == id);

            if (regular_task_ == null)
            {
                return NotFound();
            }
            else 
            {
                regular_task = regular_task_;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.regular_tasks == null)
            {
                return NotFound();
            }
            var regular_task_ = await _context.regular_tasks.FindAsync(id);

            if (regular_task_ != null)
            {
                regular_task = regular_task_;
                _context.regular_tasks.Remove(regular_task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
