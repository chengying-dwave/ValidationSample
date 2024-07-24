namespace ValidationSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    /// <summary>
    /// Gets a ViewModel showing how to use DataAnnotations for validation
    /// </summary>
    public ValidationUsingDataAnnotationViewModel ValidationUsingDataAnnotationViewModel { get;  } = new ValidationUsingDataAnnotationViewModel();
}
