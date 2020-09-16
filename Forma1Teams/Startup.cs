using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;
using Forma1Teams.Services;

namespace Forma1Teams
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 10;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			})
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<F1Context>()
			.AddDefaultTokenProviders();

			services.AddRazorPages();

			//Mivel az SQLite in-memory adatbázisok élettartama kapcsolatbontásig tart így szükséges egy explicit megnyitott kapcsolatot átadni.
			var inMemorySQLiteConnection = new SqliteConnection("DataSource=:memory:");
			inMemorySQLiteConnection.Open();

			services.AddDbContext<F1Context>(options => options.UseSqlite(inMemorySQLiteConnection));
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<TeamService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			//Mivel in-memory adatbázissal dolgozunk, mely futásidõben jön létre, így a migrációt is futásidõben kell elvégeznünk.
			UpdateDatabase(app);

			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<F1Context>();
				DbInitializer.Initialize(context, userManager, roleManager);
			}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				using (var context = serviceScope.ServiceProvider.GetService<F1Context>())
				{
					context.Database.Migrate();
				}
			}
		}
	}
}
