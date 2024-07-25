using ReactiveUI;
using System.ComponentModel.DataAnnotations;

namespace ValidationSample.ViewModels;

public class ValidationUsingDataAnnotationViewModel : ViewModelBase
{
    private string? _email;

    /// <summary>
    /// Validation using DataAnnotation
    /// </summary>
    [Required]
    [EmailAddress]
    public string? Email { get => _email; set => this.RaiseAndSetIfChanged(ref _email, value); }
}
