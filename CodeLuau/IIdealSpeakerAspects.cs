using System.Collections.Generic;

namespace CodeLuau
{
    internal interface IIdealSpeakerAspects
    {
        int? ExperienceYearCount { get; }
        bool HasBlog { get; }
        List<string> Certifications { get; }
        string Employer { get; }
    }
}
