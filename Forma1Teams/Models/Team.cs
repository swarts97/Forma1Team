using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Models
{
	public class Team
	{
		public int TeamID { get; set; }
		public string Name { get; set; }
		public int YearOfFoundation{ get; set; }
		public int WorldChampionNumber { get; set; }
		public bool EntryFeePaid { get; set; }
	}
}
