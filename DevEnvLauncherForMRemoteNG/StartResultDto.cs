using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvLauncherForMRemoteNG
{
    class StartResultDto
    {
        public IList<Executable> failedExecutables { get; set; }
        public IList<Executable> debugExecutables { get; set; }

        public StartResultDto()
        {   
            failedExecutables = new List<Executable>();
            debugExecutables = new List<Executable>();
        }


    }
}
