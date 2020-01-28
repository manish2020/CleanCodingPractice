using System.Collections.Generic;

namespace CodeLuau
{
    internal class SpeakerEvaluator
    {
        private const int MinExperienceYearCount = 11; 
        private const int MinCertificationCount = 4;
        private static readonly List<string> Employers
            = new List<string>() { "Pluralsight", "Microsoft", "Google" };

        public static bool IsIdeal(IIdealSpeakerMetrics metrics)
            => metrics.ExperienceYearCount >= MinExperienceYearCount
               || metrics.HasBlog
               || metrics.Certifications.Count >= MinCertificationCount
               || Employers.Contains(metrics.Employer);
    }
}