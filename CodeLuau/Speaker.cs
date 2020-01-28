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

		private readonly Email email = new Email();
		public string EmailAddress
		{
			get => email.Address;
			set => email.Address = value;
		}

		public QualificationMetrics QualificationMetrics { get; }
			= new QualificationMetrics();

		public string BlogURL { get; set; }
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
			var registerError = GetRegisterError();
			if (registerError != null) 
				return new RegisterResponse(registerError);

            RegistrationFee = CalculateRegistrationFee();

            var speakerId = repository.SaveSpeaker(this);
			return new RegisterResponse(speakerId);
		}

		private RegisterError? GetRegisterError()
		{
			if (IsFirstNameEmpty) return RegisterError.FirstNameRequired;
			if (IsLastNameEmpty) return RegisterError.LastNameRequired;
			if (email.IsEmpty) return RegisterError.EmailRequired;
			
			if (!MeetsStandards()) 
				return RegisterError.SpeakerDoesNotMeetStandards;

			if (!ProposedConferenceSessions.Any())
				return RegisterError.NoSessionsProvided;

			if (!HasApprovedConferenceSession())
				return RegisterError.NoSessionsApproved;

			return null;
		}

        private bool MeetsStandards()
			=> QualificationEvaluator.IsIdeal(QualificationMetrics)
			   || (email.HasAcceptableDomain() && Browser.IsAcceptable);

        private bool HasApprovedConferenceSession()
			=> ProposedConferenceSessions
				.Any(session => session.IsAboutNewTech);

        private int CalculateRegistrationFee()
        {
            var yearCount = QualificationMetrics.ExperienceYearCount ?? 0;
            
            return RegistrationFeeDefaults.VariableFeeList
                .First(fee => fee.IsQualifiedExperienceYearCount(yearCount))
                .Amount;
        }
	}

}