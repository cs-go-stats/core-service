using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    [Serializable]
    public class PreconditionFailed : ArgumentException
    {
        public string PreconditionCode { get; }

        private PreconditionFailed(in string message, string preconditionCode)
            : base(message)
        {
            PreconditionCode = preconditionCode;
        }

        protected PreconditionFailed(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static PreconditionFailed ForEthalon(in string validationMessage, in string argumentName, in string ethalonValue, in string preconditionCode) =>
            new PreconditionFailed(
                string.Format(
                    validationMessage.NotNull(nameof(validationMessage)),
                    argumentName.NotNull(nameof(argumentName)),
                    ethalonValue.NotNull(nameof(ethalonValue))),
                preconditionCode);

        public static PreconditionFailed ForCase(in string validationMessage, in string argumentName, in string preconditionCode) =>
            new PreconditionFailed(
                string.Format(
                    validationMessage.NotNull(nameof(validationMessage)),
                    argumentName.NotNull(nameof(argumentName))),
                preconditionCode);

        public static PreconditionFailed ForEthalons(in string validationMessage, in string argumentName, in string ethalon1Value, in string ethalon2Value, in string preconditionCode) =>
            new PreconditionFailed(
                string.Format(
                    validationMessage.NotNull(nameof(validationMessage)),
                    argumentName.NotNull(nameof(argumentName)),
                    ethalon1Value.NotNull(nameof(ethalon1Value)),
                    ethalon2Value.NotNull(nameof(ethalon2Value))),
                preconditionCode);
    }

    public static class PreconditionCodes
    {
        public const string GreaterThan = nameof(GreaterThan);
        public const string GreaterThanOrEqual = nameof(GreaterThanOrEqual);
        public const string LessThan = nameof(LessThan);
        public const string LessThanOrEqual = nameof(LessThanOrEqual);
        public const string EqualTo = nameof(EqualTo);
        public const string NotEqualTo = nameof(NotEqualTo);
        public const string Positive = nameof(Positive);
        public const string Negative = nameof(Negative);
        public const string Natural = nameof(Natural);
        public const string InRange = nameof(InRange);
    }
}