using Forma1Teams.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Forma1Teams.Data
{
	public class DbInitializer
	{
        public static void Initialize(F1Context context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            InitializeRoles(roleManager);
            InitializeUsers(userManager);
            InitializeTeams(context);
        }

		public static void InitializeRoles(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.RoleExistsAsync("Admin").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Admin";
				IdentityResult roleResult = roleManager.CreateAsync(role).Result;
			}

			if (!roleManager.RoleExistsAsync("User").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "User";
				IdentityResult roleResult = roleManager.CreateAsync(role).Result;
			}
		}

		static void InitializeUsers(UserManager<IdentityUser> userManager)
		{
			if (userManager.FindByNameAsync("admin").Result == null)
			{
				IdentityUser user = new IdentityUser();
				user.UserName = "admin";
				user.Email = "admin@gmail.com";
				user.EmailConfirmed = true;

				IdentityResult result = userManager.CreateAsync(user, "f1test2018").Result;

				if (result.Succeeded)
					userManager.AddToRoleAsync(user, "Admin").Wait();
			}

			if (userManager.FindByNameAsync("user").Result == null)
			{
				IdentityUser user = new IdentityUser();
				user.UserName = "user";
				user.Email = "user@gmail.com";
				user.EmailConfirmed = true;

				IdentityResult result = userManager.CreateAsync(user, "f1user2020").Result;

				if (result.Succeeded)
					userManager.AddToRoleAsync(user, "User").Wait();
			}
		}

		public static void InitializeTeams(F1Context context)
		{
            //Mivel in-memory adatbázisról van szó, ez a kódrészlet felesleges jelenleg,
            //de később ha átváltanánk SQL Serverre például már hasznos, így elhelyezése indokolt.
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
