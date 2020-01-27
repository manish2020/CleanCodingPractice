using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLuau
{
    class SpeakerEvaluator
    {
        private const int IdealExperienceYearCount = 11; 
        private const int IdealCertificationCount = 4;
        private static readonly List<string> IdealEmployers
            = new List<string>() { "Pluralsight", "Microsoft", "Google" };

        public static bool IsIdeal(Speaker speaker)
            => speaker.ExperienceYearCount >= IdealExperienceYearCount
               || speaker.HasBlog
               || speaker.Certifications.Count >= IdealCertificationCount
               || IdealEmployers.Contains(speaker.Employer);
    }
}
