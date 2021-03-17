Purpose : Console application with Dependency Injection / Serilog / Configuration


## dotnet new console -o ConsoleApplicationExample
```Powershell
cd ConsoleApplicationExample
```

## 2/ Add the nugget package: Microsoft.Extensions.Hosting
```Powershell
dotnet add package Microsoft.Extensions.Hosting --version 3.1.7
```

## 3/ Add the nuget package : Serilog
```Powershell
dotnet add package Serilog.Extensions.Hosting --version 3.1.0
```

## 4/ Add the nuget package : Serilog Configuration
```Powershell
dotnet add package Serilog.Settings.Configuration --version 3.1.0
```

## 5/ Add the nuget package : Serilog.Sinks.Console
```Powershell
dotnet add package Serilog.Sinks.Console --version 3.1.1
```

## 6/ Add the Configuration file appsettings.json

## 7/ Add the method static void BuildConfig(IConfigurationBuilder) in Program class


