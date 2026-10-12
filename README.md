# .NET Windows Service Demo

## .NET 8 and .NET 10 Windows Service

This sample targets .NET 8 and .NET 10 using the Generic Host, Windows Service integration, and `BackgroundService`.

## Project structure

- `src/DemoWindowsService/Program.cs` - Generic Host startup and Windows Service integration
- `src/DemoWindowsService/HeartbeatService.cs` - hosted service and heartbeat logging
- `src/DemoWindowsService/DemoWindowsService.csproj` - multi-target project definition

## Build

Build both target frameworks:

```powershell
dotnet build .\src\DemoWindowsService\DemoWindowsService.csproj
```

Build one target framework:

```powershell
dotnet build .\src\DemoWindowsService\DemoWindowsService.csproj -f net10.0-windows
dotnet build .\src\DemoWindowsService\DemoWindowsService.csproj -f net8.0-windows
```

## Run in console mode for testing

```powershell
dotnet run --project .\src\DemoWindowsService\DemoWindowsService.csproj -f net10.0-windows
```

The Generic Host runs as a console application when started interactively. Press `Ctrl+C` to stop it gracefully. Use `-f net8.0-windows` to run the .NET 8 build.

The service supports normal start, stop, and shutdown control. Pause and continue controls from the legacy .NET Framework sample are not implemented in this Generic Host version.

## Install as a Windows Service

Build the desired target framework, then run these commands in an elevated PowerShell session, adjusting the path for the selected framework:

```powershell
$serviceExecutable = (Resolve-Path ".\src\DemoWindowsService\bin\Debug\net10.0-windows\DemoWindowsService.exe").Path
New-Service `
    -Name "DemoWindowsService" `
    -DisplayName "Demo Windows Service" `
    -Description ".NET Windows Service sample" `
    -BinaryPathName "`"$serviceExecutable`"" `
    -StartupType Automatic
Start-Service -Name "DemoWindowsService"
```

To stop and remove the service:

```powershell
Stop-Service -Name "DemoWindowsService"
sc.exe delete "DemoWindowsService"
```

The service writes heartbeat messages every five seconds to:

```text
C:\ProgramData\DemoWindowsService\service.log
```

The service account must have write access to this directory. For production deployments, configure a dedicated least-privilege service account and grant it only the permissions it needs.

## Support lifecycle

.NET 8 support ends on November 10, 2026. .NET 10 is the recommended target for new deployments.
