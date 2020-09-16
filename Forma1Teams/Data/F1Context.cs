using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Forma1Teams.Data
{
    public class F1Context : IdentityDbContext<IdentityUser>
	{
		public readonly IHttpContextAccessor _httpContextAccessor;
		public F1Context (DbContextOptions<F1Context> options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			_httpContextAccessor = httpContextAccessor;
		}

        public DbSet<Forma1Teams.Models.Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Team>().ToTable("Team");
		}
	}
}
