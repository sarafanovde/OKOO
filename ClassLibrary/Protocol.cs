using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ProtocolIncident
    {
        public string ID;
        public List<ProtocolOne> Protocol;

        public ProtocolIncident ()
        {
            Protocol = new List<ProtocolOne>();
        }
    }
}
