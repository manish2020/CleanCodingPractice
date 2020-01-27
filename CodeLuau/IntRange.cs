using System.Diagnostics;

namespace CodeLuau
{
    [DebuggerDisplay("{DebugDisplay")]
    internal class IntRange
    {
        private string DebugDisplay
            => $"Min: {Minimum}; Max: {Maximum}";


        public int Minimum { get; }
        public int Maximum { get; }


        #region Constructors

        private IntRange(int minimum, int maximum)
            => (Minimum, Maximum) = (minimum, maximum);

        public static IntRange MinToMaxRange(int minimum, int maximum)
            => new IntRange(minimum, maximum);

        #endregion


        public bool ClosedIncludes(int year)
            => Minimum <= year && year <= Maximum;
    }
}
