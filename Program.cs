using CreateProtocol.Protocol;
using System.Runtime.Versioning;

namespace CreateProtocol
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "CreateProtocol";
            ProtocolController? protocol = new();
            if (args.Length > 0)
            {
                Console.WriteLine($"Call From Protocol:\n{args[0]}");
                _ = protocol.Execute(Method.Uninstall, name);
            } else {
                _ = protocol.Execute(Method.Install, name);
                Console.WriteLine("Press any key to exit.");
            }
            Console.ReadKey();
            return;
        }
    }
}
