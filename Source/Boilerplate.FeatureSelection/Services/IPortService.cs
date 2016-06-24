namespace Boilerplate.FeatureSelection.Services
{
    /// <summary>
    /// Gets random free ports on the current machine.
    /// </summary>
    public interface IPortService
    {
        /// <summary>
        /// Gets a random free port on the machine by randomly test ports until a free one is found.
        /// </summary>
        /// <param name="https">Find a free HTTPS port between 44300 and 44399, otherwise find a port between 1025 and 65535.</param>
        /// <returns>A port number.</returns>
        int GetRandomFreePort(bool https = false);
    }
}
