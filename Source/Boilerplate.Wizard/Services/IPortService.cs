namespace Boilerplate.Wizard.Services
{
    public interface IPortService
    {
        int GetRandomFreePort(bool ssl = false);
    }
}
