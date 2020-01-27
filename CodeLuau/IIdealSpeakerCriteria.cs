using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLuau
{
    internal interface IIdealSpeakerCriteria
    {
        int? ExperienceYearCount { get; }
        bool HasBlog { get; }
        List<string> Certifications { get; }
        string Employer { get; }
    }
}
