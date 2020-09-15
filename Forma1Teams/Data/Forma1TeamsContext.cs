using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Models;

namespace Forma1Teams.Data
{
    public class Forma1TeamsContext : DbContext
    {
        public Forma1TeamsContext (DbContextOptions<Forma1TeamsContext> options)
            : base(options)
        {
        }

        public DbSet<Forma1Teams.Models.Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Team>().ToTable("Team");
		}
	}
}
