using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Celoxis.Api.Models;

public interface IValidatable
{
    ValidationResult Validate();
}

public class ValidationError
{
    public string Property { get; set; }
    public string Message { get; set; }

    public ValidationError(string property, string message)
    {
        Property = property;
        Message = message;
    }
}

public class ValidationResult
{
    public List<ValidationError> Errors { get; }
    public bool IsValid => Errors.Count == 0;

    public ValidationResult(List<ValidationError> errors)
    {
        Errors = errors ?? new List<ValidationError>();
    }

    public ValidationResult() : this(new List<ValidationError>()) { }
}
