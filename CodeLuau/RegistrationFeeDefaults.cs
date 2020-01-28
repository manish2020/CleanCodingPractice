using System.Collections.Generic;

namespace CodeLuau
{
    internal sealed class RegistrationFeeDefaults
    {
        private static readonly RegistrationFee FeeFrom0To1
            = new RegistrationFee
            {
                QualifiedExperienceYearCount = IntRange.MinToMax(0, 1),
                Amount = 500
            };

        private static readonly RegistrationFee FeeFrom2To3
            = new RegistrationFee
            {
                QualifiedExperienceYearCount = IntRange.MinToMax(2, 3),
                Amount = 250
            };

        private static readonly RegistrationFee FeeFrom4To5
            = new RegistrationFee
            {
                QualifiedExperienceYearCount = IntRange.MinToMax(4, 5),
                Amount = 100
            };

        private static readonly RegistrationFee FeeFrom6To9
            = new RegistrationFee
            {
                QualifiedExperienceYearCount = IntRange.MinToMax(6, 9),
                Amount = 50
            };

        private const int HighestExperienceYears = 999;
        private static readonly RegistrationFee FeeFrom10
            = new RegistrationFee
            {
                QualifiedExperienceYearCount = IntRange.MinToMax(10, HighestExperienceYears),
                Amount = 250
            };


        public static readonly List<RegistrationFee> VariableFeeList
            = new List<RegistrationFee>
            {
                FeeFrom0To1,
                FeeFrom2To3,
                FeeFrom4To5,
                FeeFrom6To9,
                FeeFrom10
            };
    }
}
