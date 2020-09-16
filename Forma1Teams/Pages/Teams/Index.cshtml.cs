using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Data;
using Forma1Teams.Models;

namespace Forma1Teams.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly Forma1Teams.Data.F1Context _context;

        public IndexModel(Forma1Teams.Data.F1Context context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string FoundationSort { get; set; }
        public string WinSort { get; set; }
        public IList<Team> Teams { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            FoundationSort = sortOrder == "foundation_asc" ? "foundation_desc" : "foundation_asc";
            WinSort = sortOrder == "win_asc" ? "win_desc" : "win_asc";

            IQueryable<Team> teamsIQ = from t in _context.Teams
                                       select t;

            switch (sortOrder)
            {
                case "name_desc":
                    teamsIQ = teamsIQ.OrderByDescending(t => t.Name);
                    break;
                case "foundation_asc":
                    teamsIQ = teamsIQ.OrderBy(t => t.YearOfFoundation);
                    break;
                case "foundation_desc":
                    teamsIQ = teamsIQ.OrderByDescending(t => t.YearOfFoundation);
                    break;
                case "win_asc":
                    teamsIQ = teamsIQ.OrderBy(t => t.WorldChampionNumber);
                    break;
                case "win_desc":
                    teamsIQ = teamsIQ.OrderByDescending(t => t.WorldChampionNumber);
                    break;
                default:
                    teamsIQ = teamsIQ.OrderBy(t => t.Name);
                    break;
            }

            Teams = await teamsIQ.AsNoTracking().ToListAsync();
        }
    }
}
