namespace ValidationSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Gets a ViewModel showing how to use DataAnnotations for validation
    /// </summary>
    public ValidationUsingDataAnnotationViewModel ValidationUsingDataAnnotationViewModel { get; } = new ValidationUsingDataAnnotationViewModel();
}
