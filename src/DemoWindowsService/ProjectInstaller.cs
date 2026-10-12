using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DemoWindowsService
{
    [RunInstaller(true)]
    public sealed class ProjectInstaller : Installer
    {
        private readonly ServiceProcessInstaller _processInstaller;
        private readonly ServiceInstaller _serviceInstaller;

        public ProjectInstaller()
        {
            _processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem,
                Username = null,
                Password = null
            };

            _serviceInstaller = new ServiceInstaller
            {
                ServiceName = "DemoWindowsService",
                DisplayName = "Demo Windows Service",
                Description = ".NET Framework 4.8.1 sample Windows Service",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
