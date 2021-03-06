using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeLuau.Tests
{
	[TestClass]
	public class SpeakerTests
	{
		private readonly FakeRepository repository = new FakeRepository();

		[TestMethod]
		public void Register_EmptyFirstName_ReturnsFirstNameRequired()
		{
			//arrange
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.FirstName = "";

			//act
			var result = speaker.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.FirstNameRequired, result.Error);
		}

		[TestMethod]
		public void Register_EmptyLastName_ReturnsLastNameRequired()
		{
			//arrange
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.LastName = "";

			//act
			var result = speaker.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.LastNameRequired, result.Error);
		}

		[TestMethod]
		public void Register_EmptyEmail_ReturnsEmailRequired()
		{
			//arrange
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.EmailAddress = "";

			//act
			var result = speaker.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.EmailRequired, result.Error);
		}

		[TestMethod]
		public void Register_WorksForPrestigiousEmployerButHasRedFlags_ReturnsSpeakerId()
		{
			//arrange
			var speaker = GetSpeakerWithRedFlags();
			speaker.QualificationMetrics.Employer = "Microsoft";

			//act
			var result = speaker.TryRegister(new FakeRepository());

			//assert
			Assert.IsNotNull(result.SpeakerId);
		}

		[TestMethod]
		public void Register_HasBlogButHasRedFlags_ReturnsSpeakerId()
		{
			//arrange
			var speaker = GetSpeakerWithRedFlags();

			//act
			var result = speaker.TryRegister(new FakeRepository());

			//assert
			Assert.IsNotNull(result.SpeakerId);
		}

		[TestMethod]
		public void Register_HasCertificationsButHasRedFlags_ReturnsSpeakerId()
		{
			//arrange
			var speaker = GetSpeakerWithRedFlags();
			speaker.QualificationMetrics.Certifications = new List<string>()
		{
			"cert1",
			"cert2",
			"cert3",
			"cert4"
		};

			//act
			var result = speaker.TryRegister(new FakeRepository());

			//assert
			Assert.IsNotNull(result.SpeakerId);
		}

		[TestMethod]
		public void Register_SingleSessionThatsOnOldTech_ReturnsNoSessionsApproved()
		{
			//arrange
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.ProposedConferenceSessions = new List<ConferenceSession>() {
			new ConferenceSession("Cobol for dummies", "Intro to Cobol")
		};

			//act
			var result = speaker.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.NoSessionsApproved, result.Error);
		}

		[TestMethod]
		public void Register_NoSessionsPassed_ReturnsNoSessionsProvidedError()
		{
			//arrange
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.ProposedConferenceSessions = new List<ConferenceSession>();

			//act
			var result = speaker.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.NoSessionsProvided, result.Error);
		}

		[TestMethod]
		public void Register_DoesntAppearExceptionalAndUsingOldBrowser_ReturnsSpeakerDoesNotMeetStandards()
		{
			//arrange
			var speakerThatDoesntAppearExceptional = GetSpeakerThatWouldBeApproved();
			speakerThatDoesntAppearExceptional.QualificationMetrics.HasBlog = false;
			speakerThatDoesntAppearExceptional.Browser = new WebBrowser("IE", 6);

			//act
			var result = speakerThatDoesntAppearExceptional.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.SpeakerDoesNotMeetStandards, result.Error);
		}

		[TestMethod]
		public void Register_DoesntAppearExceptionalAndHasAncientEmail_ReturnsSpeakerDoesNotMeetStandards()
		{
			//arrange
			var speakerThatDoesntAppearExceptional = GetSpeakerThatWouldBeApproved();
			speakerThatDoesntAppearExceptional.QualificationMetrics.HasBlog = false;
			speakerThatDoesntAppearExceptional.EmailAddress = "name@aol.com";

			//act
			var result = speakerThatDoesntAppearExceptional.TryRegister(repository);

			//assert
			Assert.AreEqual(RegisterError.SpeakerDoesNotMeetStandards, result.Error);
		}

		#region Helpers
		private Speaker GetSpeakerThatWouldBeApproved()
		{

			var result = new Speaker()
			{
				FirstName = "First",
				LastName = "Last",
				EmailAddress = "example@domain.com",
				Browser = new WebBrowser("test", 1),
				BlogURL = "",
				ProposedConferenceSessions = new List<ConferenceSession>()
				{
					new ConferenceSession("test title", "test description")
				}
			};

			var qualificationMetrics = new QualificationMetrics()
			{
				Employer = "Example Employer",
				HasBlog = true,
				ExperienceYearCount = 1,
				Certifications = new System.Collections.Generic.List<string>(),
			};

			result.QualificationMetrics.Copy(qualificationMetrics);
			return result;
		}

		private Speaker GetSpeakerWithRedFlags()
		{
			var speaker = GetSpeakerThatWouldBeApproved();
			speaker.EmailAddress = "tom@aol.com";
			speaker.Browser = new WebBrowser("IE", 6);
			return speaker;
		}
		#endregion
	}

}
