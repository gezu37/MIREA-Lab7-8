using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyModels.Data;
using CompanyModels.Models;

namespace company1.Pages.contract_tasks
{
    public class DetailsModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public DetailsModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

      public CompanyModels.Models.contract_tasks contract_task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.contract_tasks == null)
            {
                return NotFound();
            }

            var contract_task_ = await _context.contract_tasks.FirstOrDefaultAsync(m => m.task_id == id);
            if (contract_task_ == null)
            {
                return NotFound();
            }
            else 
            {
                contract_task = contract_task_;
            }
            return Page();
        }
    }
}
