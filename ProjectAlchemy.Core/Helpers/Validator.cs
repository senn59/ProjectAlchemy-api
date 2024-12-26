using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.Core.Helpers;

public class ValidationHelper
{
    public static void Validate(object obj)
    {
        var valid = Validator.TryValidateObject(obj, new ValidationContext(obj), [], true);
        if (!valid)
        {
            throw new InvalidArgumentException();
        }
    }
}