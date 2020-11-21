<h1 align="center">Simple: Hosting</h1>

<div align="center">
  Making it simple to <i>customize</i> Hosting for your .NET Core 5.x+ application
</div>

<br />

<div align="center">
    <!-- License -->
    <a href="https://choosealicense.com/licenses/mit/">
    <img src="https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square" alt="License - MIT" />
    </a>
    <!-- NuGet -->
    <a href="https://www.nuget.org/packages/WorldDomination.SimpleHosting/">
    <img src="https://buildstats.info/nuget/WorldDomination.SimpleHosting" alt="NuGet" />
    </a>
    <!-- AppVeyor CI -->
    <a href="https://ci.appveyor.com/api/projects/status/SimpleHosting/branch/master?svg=true">
    <img src="https://ci.appveyor.com/api/projects/status/011jx778q0h7g2vs?svg=true" alt="AppVeyor-CI" />
    </a>
</div>

## Key Points

- :rocket: Reduces boilerplate ceremony for your `program.cs` file.
- :white_check_mark: Sets up Serilog _around_ the entire application. (:wrench: Configure all settings via your `appsettings.json` file(s))
- :white_check_mark: Simple to add some extra (helpful) log header/footer.
- :white_check_mark: Can also add logging to your `Startup` class (new with ASP.NET Core 5+)

In summary: this library makes is <b>SIMPLE</b> (by abstracting away most of the boring ceremony) to setup your .NET Core application.

---
## Installation

Package is available via NuGet.

```sh
dotnet add package WorldDomination.SimpleHosting 
```

---
## More Information:

Basically, turn your `program.cs` into this :

```
public static Task Main(string[] args)
{
    return WorldDomination.SimpleHosting.Program.Main<Startup>(args);
}
```

and you've now got some Serilog all wired up along with some nice application-wide error handling.

For more custom settings:

```
public static Task Main(string[] args)
{
    // Every Option here is optional.
    // Set what you want.
    var options = new MainOptions
    {
        CommandLineArguments = args,
        FirstLoggingInformationMessage = "~~ Sample Web Application ~~",
        LogAssemblyInformation = true,
        LastLoggingInformationMessage = "-- Sample Web Application has ended/terminated --",

        CustomPreHostRunAsyncAction = new Action<IHost>(host =>
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation($"Inside the {nameof(MainOptions.CustomPreHostRunAsyncAction)} method. Woot!");
            }
        })
    };

    return SimpleHosting.Program.Main<Startup>(options);
}
```

---

## Contribute
Yep - contributions are always welcome. Please read the contribution guidelines first.

## Code of Conduct

If you wish to participate in this repository then you need to abide by the code of conduct.

## Feedback

Yes! Please use the Issues section to provide feedback - either good or needs improvement :cool:

---

