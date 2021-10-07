namespace WorldDomination.SimpleHosting;

public class MainOptions<TStartup> where TStartup : class
{
    /// <summary>
    /// Command line arguments.
    /// </summary>
    public string[] CommandLineArguments { get; set; }

    /// <summary>
    /// Optional text which is first displayed when the application starts.
    /// </summary>
    /// <remarks>This can be useful to help determine if things have started and are working ok.</remarks>
    public string FirstLoggingInformationMessage { get; set; }

    /// <summary>
    /// Write the assembly name, version and date information to the logger?
    /// </summary>
    public bool LogAssemblyInformation { get; set; } = true;

    /// <summary>
    /// Optional text which is last displayed when the application stops.
    /// </summary>
    /// <remarks>This could be useful to help determine when things are finally stopping.</remarks>
    public string LastLoggingInformationMessage { get; set; }

    public Func<WebHostBuilderContext, ILogger, TStartup> StartupActivation { get; set; }
}
