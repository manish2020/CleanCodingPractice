using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLuau
{
	public class Speaker
	{
		public string FirstName { get; set; }
		private bool IsFirstNameEmpty => string.IsNullOrWhiteSpace(FirstName);

		public string LastName { get; set; }
		private bool IsLastNameEmpty => string.IsNullOrWhiteSpace(LastName);

		public string Email { get; set; }
		private bool IsEmailEmpty => string.IsNullOrWhiteSpace(Email);

		public int? Exp { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
		public List<Session> Sessions { get; set; }

		public RegisterResponse Register(IRepository repository)
		{
			int? speakerId = null;
			bool good = false;
			bool appr = false;
			var ot = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

			var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

			if (IsFirstNameEmpty)
				return new RegisterResponse(RegisterError.FirstNameRequired);
			if (IsLastNameEmpty)
				return new RegisterResponse(RegisterError.LastNameRequired);
			if (IsEmailEmpty)
				return new RegisterResponse(RegisterError.EmailRequired);


			var emps = new List<string>() { "Pluralsight", "Microsoft", "Google" };

			good = Exp > 10 || HasBlog || Certifications.Count() > 3 || emps.Contains(Employer);

			if (!good)
			{
				string emailDomain = Email.Split('@').Last();

				if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
				{
					good = true;
				}
			}

			if (good)
			{
				if (Sessions.Count() != 0)
				{
					foreach (var session in Sessions)
					{
						foreach (var tech in ot)
						{
							if (session.Title.Contains(tech) || session.Description.Contains(tech))
							{
								session.Approved = false;
								break;
							}
							else
							{
								session.Approved = true;
								appr = true;
							}
						}
					}
				}
				else
				{
					return new RegisterResponse(RegisterError.NoSessionsProvided);
				}

				if (appr)
				{
					//if we got this far, the speaker is approved
					//let's go ahead and register him/her now.
					//First, let's calculate the registration fee. 
					//More experienced speakers pay a lower fee.
					if (Exp <= 1)
					{
						RegistrationFee = 500;
					}
					else if (Exp >= 2 && Exp <= 3)
					{
						RegistrationFee = 250;
					}
					else if (Exp >= 4 && Exp <= 5)
					{
						RegistrationFee = 100;
					}
					else if (Exp >= 6 && Exp <= 9)
					{
						RegistrationFee = 50;
					}
					else
					{
						RegistrationFee = 0;
					}


					try
					{
						speakerId = repository.SaveSpeaker(this);
					}
					catch (Exception e)
					{
						//in case the db call fails 
					}
				}
				else
				{
					return new RegisterResponse(RegisterError.NoSessionsApproved);
				}
			}
			else
			{
				return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
			}

 
            return new RegisterResponse((int)speakerId);
		}
	}
}