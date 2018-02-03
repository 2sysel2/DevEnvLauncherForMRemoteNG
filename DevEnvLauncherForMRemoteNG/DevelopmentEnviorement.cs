using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvLauncherForMRemoteNG
{
    class DevelopmentEnviorement
    { 
        public string Name { get; set; }
        public IList<Executable> Configurations { get; set; }

        public DevelopmentEnviorement(string name)
        {
            Name = name;
            Configurations = new List<Executable>();
        }

        public bool IsEnviorement(string name)
        {
            return Name.Equals(name);
        }

        public bool Start()
        {
            bool failureDetected = false;
            foreach (Executable executable in Configurations)
            {
                if (!executable.execute())
                {
                    failureDetected = true;
                }
            }
            return !failureDetected;
        }
    }
}
