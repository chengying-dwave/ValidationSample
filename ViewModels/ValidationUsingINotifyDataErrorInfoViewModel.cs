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

    // we have errors present if errors.Count is greater than 0
    public bool HasErrors => errors.Count > 0;

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
