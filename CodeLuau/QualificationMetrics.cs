using System.Collections.Generic;

namespace CodeLuau
{
    public class QualificationMetrics
    {
        public int? ExperienceYearCount { get; set; }
        public bool HasBlog { get; set; }
        public List<string> Certifications { get; set; }
        public string Employer { get; set; }

        public void Copy(QualificationMetrics metrics)
        {
            ExperienceYearCount = metrics.ExperienceYearCount;
            HasBlog = metrics.HasBlog;
            Certifications = metrics.Certifications;
            Employer = metrics.Employer;
        }
    }
}