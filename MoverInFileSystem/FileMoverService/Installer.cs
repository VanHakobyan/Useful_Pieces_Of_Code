using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace FileMoverService
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            this.Installers.Add(new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem });
            this.Installers.Add(new ServiceInstaller { ServiceName = "BetConstruct.FileMover", Description = "BetConstruct File Mover", StartType = ServiceStartMode.Manual });
        }
    }
}
