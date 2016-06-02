namespace Boilerplate.Wizard.Services
{
    public interface IFileFixerService
    {
        string[] FileExtensions { get; }

        string Fix(string content);
    }
}
