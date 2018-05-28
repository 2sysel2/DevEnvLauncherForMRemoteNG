using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvLauncherForMRemoteNG
{
    class Executable
    {
        public Executable(string path,string arguments,bool debug)
        {
            Path = path;
            Arguments = arguments;
            Debug = debug;
        }

        public string Path { get; set; }
        public string Arguments { get; set; }
        public bool? Debug { get; set; }

        public bool execute()
        {
            Process process = new Process();
            process.StartInfo.FileName = Path;
            process.StartInfo.Arguments = Arguments;
            try
            {
                return process.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to start [{0}] with arguments [{1}]",Path,Arguments);
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}
