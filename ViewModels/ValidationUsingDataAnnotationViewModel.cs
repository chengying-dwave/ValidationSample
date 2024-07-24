using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ValidationSample.ViewModels;

public partial class ValidationUsingDataAnnotationViewModel : ObservableValidator
{
    /// <summary>
    /// Validation using DataAnnotation
    /// </summary>
    [Required]
    [EmailAddress]
    [ObservableProperty]
    private string? _email;
}
