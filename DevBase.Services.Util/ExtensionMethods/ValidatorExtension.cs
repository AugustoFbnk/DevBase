using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace DevBase.Services.Util.ExtensionMethods
{
    public static class ValidatorExtension
    {
        public static IEnumerable<string> ToListValidationFailureString(this IEnumerable<ValidationFailure> Errors)
        {
            if (Errors != null && Errors.Any())
            {
                return Errors.Select(item => item.ErrorMessage);
            }
            return new List<string>();
        }
    }
}
