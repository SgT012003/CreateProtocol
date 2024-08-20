using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateProtocol.Protocol
{
    public interface IProtocolController
    {
        public Task<bool> Execute(Method method, string name);
    }
}
