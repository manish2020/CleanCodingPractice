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

		public int? ExperienceYearCount { get; set; }
        public bool HasBlog { get; set; }
        public List<string> Certifications { get; set; }
        public string BlogURL { get; set; }
        public string Employer { get; set; }

        public WebBrowser Browser { get; set; }
		public int RegistrationFee { get; set; }
		public List<Session> Sessions { get; set; }

		public RegisterResponse TryRegister(IRepository repository)
		{
            try
            {
				return Register(repository);
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex);
				throw;
            }
        }

        private RegisterResponse Register(IRepository repository)
        {
            if (IsFirstNameEmpty)
				return new RegisterResponse(RegisterError.FirstNameRequired);
			if (IsLastNameEmpty)
				return new RegisterResponse(RegisterError.LastNameRequired);
			if (IsEmailEmpty)
				return new RegisterResponse(RegisterError.EmailRequired);

            var isIdeal = SpeakerEvaluator.IsIdeal(this);

			if (!isIdeal)
			{
				string emailDomain = Email.Split('@').Last();

                var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };
				if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
				{
					isIdeal = true;
				}
			}

            int? speakerId = null;
			if (isIdeal)
			{
                bool appr = false;
				if (Sessions.Count() != 0)
				{
					foreach (var session in Sessions)
					{
                        var ot = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
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
                    var valuedExp = ExperienceYearCount ?? 0;
                    RegistrationFee 
                        = RegistrationFeeDefaults.VariableFeeList
                        .First(fee => fee.IsQualifiedExperienceYears(valuedExp))
                        .Amount;

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