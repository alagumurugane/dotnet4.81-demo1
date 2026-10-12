# Demo .NET Framework 4.8.1 Windows Service

This sample demonstrates a minimal Windows Service targeting .NET Framework 4.8.1.

## Project structure

- `src/DemoWindowsService/Program.cs` – service implementation and console-mode test host
- `src/DemoWindowsService/ProjectInstaller.cs` – service installer definition
- `src/DemoWindowsService/DemoWindowsService.csproj` – project definition for .NET Framework 4.8.1

## Build

```powershell
dotnet build src/DemoWindowsService/DemoWindowsService.csproj
```

## Run in console mode for testing

```powershell
src\DemoWindowsService\bin\Debug\DemoWindowsService.exe /console
```

## Install as a Windows Service

From an elevated Developer Command Prompt:

```powershell
InstallUtil.exe "C:\path\to\DemoWindowsService.exe"
```

To uninstall:

```powershell
InstallUtil.exe /u "C:\path\to\DemoWindowsService.exe"
```

The service writes heartbeat messages to:

```text
C:\ProgramData\DemoWindowsService\service.log
```
