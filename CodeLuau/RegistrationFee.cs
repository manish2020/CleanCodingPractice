using System.Diagnostics;

namespace CodeLuau
{
    [DebuggerDisplay("{DebugDisplay")]
    internal class RegistrationFee
    {
        private string DebugDisplay
            => $"Years: [{QualifiedExperienceYearCount.Minimum}, "
               + $"{QualifiedExperienceYearCount.Maximum}], Amt: {Amount}";


        public IntRange QualifiedExperienceYearCount { get; set; }
        public int Amount { get; set; }

        public bool IsQualifiedExperienceYearCount(int year)
            => QualifiedExperienceYearCount.ClosedIncludes(year);
    }
}
