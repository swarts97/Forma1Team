using Forma1Teams.Data;
using Forma1Teams.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Services
{
	public class TeamService
	{
        public Team Team { get; set; }
		public string NameSort { get; set; }
		public string FoundationSort { get; set; }
		public string WinSort { get; set; }
		public IList<Team> Teams { get; set; }

		public async Task TeamByIdAsync(F1Context _context, int? id)
		{
			Team = await _context.Teams.FirstOrDefaultAsync(m => m.TeamID == id);
		}

		public void FillTeamData(string Name, int YearOfFoundation, int WorldChampionNumber, bool EntryFeePaid)
		{
			Team.Name = Name;
			Team.YearOfFoundation = YearOfFoundation;
			Team.WorldChampionNumber = WorldChampionNumber;
			Team.EntryFeePaid = EntryFeePaid;
		}

		public Team CreateNewTeam(string Name, int YearOfFoundation, int WorldChampionNumber, bool EntryFeePaid)
		{
			return new Team()
			{
				Name = Name,
				YearOfFoundation = YearOfFoundation,
				WorldChampionNumber = WorldChampionNumber,
				EntryFeePaid = EntryFeePaid
			};
		}

		public async Task TeamsQuery(F1Context _context, string sortOrder)
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
