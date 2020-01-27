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
		public List<ConferenceSession> ProposedConferenceSessions { get; set; }

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
            if (!MeetsStandards())
                return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
            if (!ProposedConferenceSessions.Any())
                return new RegisterResponse(RegisterError.NoSessionsProvided);
            if (!HasApprovedConferenceSession())
                return new RegisterResponse(RegisterError.NoSessionsApproved);


            int? speakerId = null;
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

            return new RegisterResponse((int)speakerId);
		}

        private bool MeetsStandards()
            => IdealSpeakerCriteria.IsIdeal(this)
               || (email.HasAcceptableDomain() && Browser.IsAcceptable);

        private bool HasApprovedConferenceSession()
            => ProposedConferenceSessions
                .Any(session => session.IsAboutNewTech);
    }
}