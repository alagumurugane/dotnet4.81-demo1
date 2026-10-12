using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace DemoWindowsService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && string.Equals(args[0], "/console", StringComparison.OrdinalIgnoreCase))
            {
                RunInConsole();
                return;
            }

            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(new DemoWindowsService());
                return;
            }

            Console.WriteLine("DemoWindowsService is a .NET Framework 4.8.1 Windows Service sample.");
            Console.WriteLine("Use '/console' to run it in the foreground for local testing.");
            Console.WriteLine("Use InstallUtil.exe to install it as a Windows Service.");
        }

        private static void RunInConsole()
        {
            var service = new DemoWindowsService();
            service.StartConsoleMode();

            Console.WriteLine("Service is running in console mode. Press Enter to stop.");
            Console.ReadLine();

            service.StopConsoleMode();
        }
    }

    public sealed class DemoWindowsService : ServiceBase
    {
        private readonly Timer _timer;
        private readonly string _logPath;

        public DemoWindowsService()
        {
            ServiceName = "DemoWindowsService";
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;

            _logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "DemoWindowsService",
                "service.log");

            _timer = new Timer(5000);
            _timer.Elapsed += OnTimerElapsed;
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("Service started.");
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            WriteLog("Service stopped.");
        }

        protected override void OnPause()
        {
            _timer.Stop();
            WriteLog("Service paused.");
        }

        protected override void OnContinue()
        {
            WriteLog("Service continuing.");
            _timer.Start();
        }

        public void StartConsoleMode()
        {
            WriteLog("Console mode started.");
            _timer.Start();
        }

        public void StopConsoleMode()
        {
            _timer.Stop();
            WriteLog("Console mode stopped.");
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            WriteLog($"Heartbeat at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        private void WriteLog(string message)
        {
            var directory = Path.GetDirectoryName(_logPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.AppendAllText(_logPath, $"{DateTime.Now:O} - {message}{Environment.NewLine}");
        }
    }
}
