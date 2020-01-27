using System.Collections.Generic;

namespace CodeLuau
{
    class SpeakerEvaluator
    {
        private const int IdealExperienceYearCount = 11; 
        private const int IdealCertificationCount = 4;
        private static readonly List<string> IdealEmployers
            = new List<string>() { "Pluralsight", "Microsoft", "Google" };

        public static bool IsIdeal(IIdealSpeakerCriteria criteria)
            => criteria.ExperienceYearCount >= IdealExperienceYearCount
               || criteria.HasBlog
               || criteria.Certifications.Count >= IdealCertificationCount
               || IdealEmployers.Contains(criteria.Employer);
    }
}
