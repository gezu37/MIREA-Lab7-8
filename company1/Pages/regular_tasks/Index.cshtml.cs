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
    public class IndexModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public IndexModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        public IList<regular_task> regular_task { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.regular_tasks != null)
            {
                regular_task = await _context.regular_tasks
                .Include(r => r.contact).ToListAsync();
            }
        }
    }
}
