using System;
using System.Collections.Generic;
using System.Text;
using DemoAppliaction.Core.Application.Exceptions;

namespace DemoAppliaction.Core.Application.Wrappers;
public class ValidationErrorResponse : Response<IEnumerable<ValidationErrorAsKeyValue>>
{
    public bool IsValidationError { get; set; }

    public ValidationErrorResponse()
    {
        IsValidationError = true;
    }
}