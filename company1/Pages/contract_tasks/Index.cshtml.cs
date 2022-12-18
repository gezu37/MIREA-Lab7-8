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
    public class IndexModel : PageModel
    {
        private readonly CompanyModels.Data.companyContext _context;

        public IndexModel(CompanyModels.Data.companyContext context)
        {
            _context = context;
        }

        public IList<CompanyModels.Models.contract_tasks> contract_task { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.contract_tasks != null)
            {
                contract_task = await _context.contract_tasks
                .Include(c => c.contact)
                .Include(c => c.contract)
                .Include(c => c.equipment).ToListAsync();
            }
        }
    }
}
