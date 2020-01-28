using System.Collections.Generic;

namespace CodeLuau
{
    internal class QualificationEvaluator
    {
        private const int MinIdealExperienceYearCount = 11; 
        private const int MinIdealCertificationCount = 4;
        private static readonly List<string> IdealEmployers
            = new List<string>() { "Pluralsight", "Microsoft", "Google" };

        public static bool IsIdeal(QualificationMetrics metrics)
            => metrics.ExperienceYearCount >= MinIdealExperienceYearCount
               || metrics.HasBlog
               || metrics.Certifications.Count >= MinIdealCertificationCount
               || IdealEmployers.Contains(metrics.Employer);

    }
}