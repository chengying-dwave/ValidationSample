using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ValidationSample.ViewModels;

public class ValidationUsingINotifyDataErrorInfoViewModel : ViewModelBase, INotifyDataErrorInfo
{
    // Store Errors in a Dictionary
    private readonly Dictionary<string, List<ValidationResult>> errors = [];

    private string? _email;

    public ValidationUsingINotifyDataErrorInfoViewModel()
    {
        // Listen to changes of "ValidationUsingINotifyDataErrorInfo" and re-evaluate the validation
        this.WhenAnyValue(x => x.Email)
            .Subscribe(_ => ValidateEmail());

        // run INotifyDataErrorInfo-validation on start-up
        ValidateEmail();
    }

    private void ValidateEmail()
    {
        // first of all clear all previous errors
        ClearErrors(nameof(Email));

        // No empty string allowed
        if (string.IsNullOrEmpty(Email))
        {
            AddError(nameof(Email), "This field is required");
        }

        // @-sign required
        if (Email is null || !Email.Contains('@'))
        {
            AddError(nameof(Email), "Don't forget the '@'-sign");
        }
    }

    /// <summary>
    /// Clears the errors for a given property name.
    /// </summary>
    /// <param name="propertyName">The name of the property to clear or all properties if <see langword="null"/></param>
    protected void ClearErrors(string? propertyName = null)
    {
        // Clear entity-level errors when the target property is null or empty
        if (string.IsNullOrEmpty(propertyName))
        {
            errors.Clear();
        }
        else
        {
            errors.Remove(propertyName);
        }

        // Notify that errors have changed
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    /// <summary>
    /// Adds a given error message for a given property name.
    /// </summary>
    /// <param name="propertyName">the name of the property</param>
    /// <param name="errorMessage">the error message to show</param>
    /// <exception cref="NotImplementedException"></exception>
    protected void AddError(string propertyName, string errorMessage)
    {
        // Add the cached errors list for later use.
        if (!errors.TryGetValue(propertyName, out List<ValidationResult>? propertyErrors))
        {
            propertyErrors = new List<ValidationResult>();
            errors.Add(propertyName, propertyErrors);
        }

        propertyErrors.Add(new ValidationResult(errorMessage));

        // Notify that errors have changed
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    // we have errors present if errors.Count is greater than 0
    public bool HasErrors => errors.Count > 0;

    /// <summary>
    /// A property that is validated using INotifyDataErrorInfo
    /// </summary>
    public string? Email
    { get { return _email; } set { this.RaiseAndSetIfChanged(ref _email, value); } }

    // Implement members of INotifyDataErrorInfo
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <inheritdoc/>
    public IEnumerable GetErrors(string? propertyName)
    {
        // Get entity-level errors when the target property is null or empty
        if (string.IsNullOrEmpty(propertyName))
        {
            return errors.Values.SelectMany(static errors => errors);
        }

        // Property-level errors, if any
        if (errors.TryGetValue(propertyName!, out List<ValidationResult>? result))
        {
            return result;
        }

        // In case there are no errors we return an empty array.
        return Array.Empty<ValidationResult>();
    }
}
