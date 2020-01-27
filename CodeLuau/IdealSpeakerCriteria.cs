using System.Collections.Generic;

namespace CodeLuau
{
    internal class IdealSpeakerCriteria
    {
        private const int MinExperienceYearCount = 11; 
        private const int MinCertificationCount = 4;
        private static readonly List<string> Employers
            = new List<string>() { "Pluralsight", "Microsoft", "Google" };

        public static bool IsIdeal(IIdealSpeakerAspects aspects)
            => aspects.ExperienceYearCount >= MinExperienceYearCount
               || aspects.HasBlog
               || aspects.Certifications.Count >= MinCertificationCount
               || Employers.Contains(aspects.Employer);
    }
}
