using System.Text.RegularExpressions;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class InstanceValidationExtensions
    {
        public static Match ForSucceeded(this Match x) =>
            x.Success 
                ? x 
                : throw new ExpressionNotMatched();
    }
}