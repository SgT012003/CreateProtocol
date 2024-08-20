using Microsoft.Win32;
using System.Security.Principal;
using System.Runtime.Versioning;
using CreateProtocol.Protocol;

namespace CreateProtocol.Protocol
{
    
    public enum Method : byte
    {
        Install = 0,
        Uninstall = 1
    }
    [SupportedOSPlatform("windows")]
    public class ProtocolController : IProtocolController
    {
        public async Task<bool> Execute(Method method, string name)
        {
            return method switch
            {
                Method.Install => await Install(name),
                Method.Uninstall => await Uninstall(name),
                _ => false
            };
        }
        private async Task<bool> Install(string name)
        {
            await Task.Delay(0);

            // Get the path of the current process
            string ProcessPath = Path.Combine(Environment.CurrentDirectory, $"{name}.exe");

            // Verify if already exists
            if (Registry.ClassesRoot.OpenSubKey(name) != null)
            {
                Console.WriteLine("The protocol already exists.");
                return false;
            }

            // Verify if has admin rights
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("You need to run this application as administrator.");
                return false;
            }

            // Create the registry key
            try
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(name);
                key.SetValue("", "URL:serverforge protocol");
                key.SetValue("URL Protocol", "");
                key.CreateSubKey("DefaultIcon").SetValue("", ProcessPath);
                key.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command").SetValue("", $"\"{ProcessPath}\" \"%1\"");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create the registry key.\n {ex.Message}");
                return false;
            }
        }
        private async Task<bool> Uninstall(string name)
        {
            await Task.Delay(0);

            // Verify if already exists
            if (Registry.ClassesRoot.OpenSubKey(name) == null)
            {
                Console.WriteLine("The protocol doesn't exists.");
                return false;
            }

            // Verify if has admin rights
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("You need to run this application as administrator.");
                return false;
            }

            // Create the registry key
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(name);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete the registry key.\n {ex.Message}");
                return false;
            }
        }
    }
}
