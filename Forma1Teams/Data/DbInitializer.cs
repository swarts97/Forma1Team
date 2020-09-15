using Forma1Teams.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Data
{
	public class DbInitializer
	{
        public static void Initialize(F1Context context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Teams.Any())
            {
                return;
            }

            var teams = new Team[]
            {
                new Team{Name = "Mercedes-AMG Petronas F1 Team", YearOfFoundation = 1970, WorldChampionNumber = 6, EntryFeePaid = true},
                new Team{Name = "Aston Martin Red Bull Racing", YearOfFoundation = 1997, WorldChampionNumber = 4, EntryFeePaid = true},
                new Team{Name = "McLaren F1 Team", YearOfFoundation = 1966, WorldChampionNumber = 8, EntryFeePaid = true},
                new Team{Name = "Renault DP World F1 Team", YearOfFoundation = 1986, WorldChampionNumber = 2, EntryFeePaid = true},
                new Team{Name = "Scuderia Ferrari Mission Winnow", YearOfFoundation = 1950, WorldChampionNumber = 16, EntryFeePaid = true},
                new Team{Name = "Alfa Romeo Racing ORLEN", YearOfFoundation = 1993, WorldChampionNumber = 0, EntryFeePaid = true},
                new Team{Name = "Haas F1 Team", YearOfFoundation = 2016, WorldChampionNumber = 0, EntryFeePaid = true},
                new Team{Name = "Williams Racing", YearOfFoundation = 1978, WorldChampionNumber = 9, EntryFeePaid = true}
            };
            context.Teams.AddRange(teams);
            context.SaveChanges();
        }
    }
}
