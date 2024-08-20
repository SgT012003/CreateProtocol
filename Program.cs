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
            _ = protocol.Execute(Method.Install, name);
            if (args.Length > 0)
            {
                Console.WriteLine($"Call From Protocol:\n{args[0]}");
                _ = protocol.Execute(Method.Uninstall, name);
            }
            return;
        }
    }
}
