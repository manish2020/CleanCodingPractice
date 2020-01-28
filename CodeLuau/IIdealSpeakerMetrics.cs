using System.Collections.Generic;

namespace CodeLuau
{
    internal interface IIdealSpeakerMetrics
    {
        int? ExperienceYearCount { get; }
        bool HasBlog { get; }
        List<string> Certifications { get; }
        string Employer { get; }
    }
}
