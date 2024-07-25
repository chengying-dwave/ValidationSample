using ReactiveUI;
using System;

namespace ValidationSample.ViewModels;

public class ValidationUsingExceptionInsideSetterViewModel : ViewModelBase
{
    private string? _email;

    /// <summary>
    /// Validation using Exceptions (only inside setter allowed!)
    /// </summary>
    public string? Email
    {
        get { return _email; }
        set
        {
            // The field may not be null or empty
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(Email), "This field is required");
            }
            // The field must contain an '@' sign
            else if (!value.Contains('@'))
            {
                throw new ArgumentException("NOt a valid E-Mail-Address", nameof(Email));
            }
            // The checks were successful, so we can store the value
            else
            {
                this.RaiseAndSetIfChanged(ref _email, value);
            }
        }
    }
}
