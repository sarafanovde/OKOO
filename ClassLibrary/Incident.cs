using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Incident
    {
        private string number;
        private string solution;
        private bool k2;
        private bool unlawfulK2;
        public string Number 
        {
        get { return number; }
        }
        
        public string Solution
        {
        get { return solution; }
        }

        public bool K2
        {
            get { return k2; }
        }
        public bool UnlawfulK2
        {
            get { return unlawfulK2; }
        }

        public AuditIncident audit;
        public ProtocolIncident protocol;
        public string BadProtocol = "1";

        public Incident(string number, string solution, bool k2, bool unlawfulk2)
        {
            this.number = number;
            this.solution = solution;
            this.k2 = k2;
            this.unlawfulK2 = unlawfulk2;

        }

    }
}
