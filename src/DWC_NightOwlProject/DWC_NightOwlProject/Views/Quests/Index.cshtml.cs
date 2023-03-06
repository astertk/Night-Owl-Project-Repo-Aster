using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.Views.Quests
{
    public class IndexModel : PageModel
    {
        private readonly DWC_NightOwlProject.Data.WebAppDbContext _context;

        public IndexModel(DWC_NightOwlProject.Data.WebAppDbContext context)
        {
            _context = context;
        }

        public IList<Material> Material { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Materials != null)
            {
                Material = await _context.Materials.ToListAsync();
            }
        }
    }
}
