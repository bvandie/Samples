using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellPath
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPath();
        }

        static void TestPath()
        {
            Collection<PSObject> output;
            using (var runspace = RunspaceFactory.CreateOutOfProcessRunspace(new TypeTable(new string[0])))
            {
                runspace.Open();

                using (PowerShell powerShellInstance = PowerShell.Create())
                {
                    powerShellInstance.Runspace = runspace;

                    var script = @"
pwd                    
$PSScriptRoot
";

                    powerShellInstance.AddScript(script);

                    output = powerShellInstance.Invoke();
                }

                Collection<PSObject> outputViaScript;
                using (PowerShell powerShellInstance = PowerShell.Create())
                {
                    powerShellInstance.Runspace = runspace;
                    string filePath;
                    if (File.Exists("TestPath.ps1"))
                    {
                        var fileInfo = new FileInfo("TestPath.ps1");
                        filePath = fileInfo.FullName;
                        powerShellInstance.Commands.AddCommand(filePath);
                        powerShellInstance.AddParameter("WriteHello");
                        outputViaScript = powerShellInstance.Invoke();
                    }
                    
                }

                var fileExists = File.Exists("TestPath.ps1");
            }
        }
    }
}
