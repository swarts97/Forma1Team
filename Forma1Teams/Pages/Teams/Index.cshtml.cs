using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Data;
using Forma1Teams.Models;
using Forma1Teams.Services;

namespace Forma1Teams.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly F1Context _context;
        public readonly TeamService _teamService;

        public IndexModel(F1Context context, TeamService teamService)
        {
            _context = context;
            _teamService = teamService;
        }

        public async Task OnGetAsync(string sortOrder)
        {
            await _teamService.TeamsQuery(_context, sortOrder);
        }
    }
}
