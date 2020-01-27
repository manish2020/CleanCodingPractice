using System.Diagnostics;

namespace CodeLuau
{
    [DebuggerDisplay("{DebugDisplay")]
    internal class RegistrationFee
    {
        private string DebugDisplay
            => $"Years: [{QualifiedExperienceYears.Minimum}, "
               + $"{QualifiedExperienceYears.Maximum}], Amt: {Amount}";


        public IntRange QualifiedExperienceYears { get; set; }
        public int Amount { get; set; }

        public bool IsQualifiedExperienceYears(int year)
            => QualifiedExperienceYears.ClosedIncludes(year);
    }
}
