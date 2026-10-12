using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoWindowsService;

public sealed class HeartbeatService : BackgroundService
{
    private readonly ILogger<HeartbeatService> _logger;
    private readonly string _logDirectory;
    private readonly string _logPath;

    public HeartbeatService(ILogger<HeartbeatService> logger)
    {
        _logger = logger;
        _logDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "DemoWindowsService");
        _logPath = Path.Combine(_logDirectory, "service.log");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            WriteLog("Service started.");

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                WriteLog($"Heartbeat at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            WriteLog("Service stopped.");
        }
    }

    private void WriteLog(string message)
    {
        _logger.LogInformation("{Message}", message);
        Directory.CreateDirectory(_logDirectory);
        File.AppendAllText(_logPath, $"{DateTime.Now:O} - {message}{Environment.NewLine}");
    }
}
