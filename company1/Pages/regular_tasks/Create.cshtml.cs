using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyModels.Data;
using CompanyModels.Models;

namespace company1.Pages.regular_tasks
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
            return Page();
        }

        [BindProperty]
        public regular_task regular_task { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            regular_task.created = regular_task.created.ToUniversalTime();
            regular_task.deadline = regular_task.deadline.GetValueOrDefault().ToUniversalTime();

            _context.regular_tasks.Add(regular_task);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
