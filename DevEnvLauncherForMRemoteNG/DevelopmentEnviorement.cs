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

        public StartResultDto Start()
        {
            StartResultDto result = new StartResultDto();

            foreach (Executable executable in Configurations)
            {
                if (!executable.execute())
                {
                    result.failedExecutables.Add(executable);
                }

                if (executable.Debug.GetValueOrDefault(false))
                {
                    result.debugExecutables.Add(executable);
                }
            }
            return result;
        }
    }
}
