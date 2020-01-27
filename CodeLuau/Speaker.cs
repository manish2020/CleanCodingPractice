using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLuau
{
	public class Speaker : IIdealSpeakerAspects
	{
		public string FirstName { get; set; }
		private bool IsFirstNameEmpty => string.IsNullOrWhiteSpace(FirstName);

		public string LastName { get; set; }
		private bool IsLastNameEmpty => string.IsNullOrWhiteSpace(LastName);             

		private readonly Email email = new Email();
        public string EmailAddress
        {
            get => email.Address;
            set => email.Address = value;
        }

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
			if (email.IsEmpty)
				return new RegisterResponse(RegisterError.EmailRequired);

            var isIdeal = IdealSpeakerCriteria.IsIdeal(this)
                || (email.HasAcceptableDomain() && Browser.IsAcceptable);

            if (!isIdeal)
                return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
            if (!Sessions.Any())
                return new RegisterResponse(RegisterError.NoSessionsProvided);

            bool appr = false;
            foreach (var session in Sessions)
            {
                var ot = new List<string>() {"Cobol", "Punch Cards", "Commodore", "VBScript"};
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

            int? speakerId = null;
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



            return new RegisterResponse((int)speakerId);
		}


	}
}