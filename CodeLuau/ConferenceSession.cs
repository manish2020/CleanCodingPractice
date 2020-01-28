using System.Collections.Generic;
using System.Linq;

namespace CodeLuau
{
	public class ConferenceSession
	{
		private static readonly List<string> OldTechKeywordList 
			= new List<string>() {"Cobol", "Punch Cards", "Commodore", "VBScript"};

		public string Title { get; set; }
		public string Description { get; set; }
		
		public bool IsAboutNewTech
			=> !IsAboutOldTech;

		public bool IsAboutOldTech
			=> OldTechKeywordList
				.Any(tech => Title.Contains(tech) || Description.Contains(tech));

		public ConferenceSession(string title, string description)
		{
			Title = title;
			Description = description;
		}

		
	}
}
