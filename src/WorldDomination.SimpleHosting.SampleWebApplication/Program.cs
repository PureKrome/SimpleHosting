namespace WorldDomination.SimpleHosting.SampleWebApplication;

public class Program
{
    public static Task Main(string[] args)
    {
        var options = new MainOptions<Startup>
        {
            CommandLineArguments = args,
            FirstLoggingInformationMessage = "~~ Sample Web Application ~~",
            LogAssemblyInformation = true,
            LastLoggingInformationMessage = "-- Sample Web Application has ended/terminated --",
            StartupActivation = new System.Func<WebHostBuilderContext, ILogger, Startup>((context, logger) => new Startup(context.Configuration, logger))
        };

        return SimpleHosting.Program.Main(options);
    }
}
