using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DevEnvLauncherForMRemoteNG
{
    class Program
    {
        private static readonly string CONFIGURATION_FILE = "dev_env_configurations.json";

        static void Main(string[] args)
        {
            if (!File.Exists(CONFIGURATION_FILE))
            {
                createSampleConfig();
                return;
            }

            List<string> configurationsToLaunch = new List<string>();

            if(args.Length == 0)
            {
                Console.WriteLine("No Configuration Name Passed");
            }
            else
            {
                configurationsToLaunch.AddRange(args);
                foreach (string arg in args)
                {
                    Console.WriteLine("Configuration to launch [{0}]", arg);
                }
            }            

            IList<DevelopmentEnviorement> enviorements = JsonConvert.DeserializeObject <List<DevelopmentEnviorement>>(File.ReadAllText(CONFIGURATION_FILE));

            foreach(DevelopmentEnviorement enviorement in enviorements)
            {
                if (configurationsToLaunch.Contains(enviorement.Name))
                {
                    enviorement.Start();
                }
            }

            Console.ReadLine();
            
        }

        static void createSampleConfig()
        {
            Console.WriteLine("Creating sample config [{0}]",CONFIGURATION_FILE);
            File.Create(CONFIGURATION_FILE).Close();

            List<DevelopmentEnviorement> enviorements = new List<DevelopmentEnviorement>();
            DevelopmentEnviorement se1 = new DevelopmentEnviorement("sampleConfig1");
            se1.Configurations.Add(new Executable("C:\\path\\to\\executable1", "params"));
            se1.Configurations.Add(new Executable("C:\\path\\to\\executable2", "params"));
            DevelopmentEnviorement se2 = new DevelopmentEnviorement("sampleConfig2");
            se2.Configurations.Add(new Executable("C:\\path\\to\\executable3", "params"));
            se2.Configurations.Add(new Executable("C:\\path\\to\\executable4", "params"));

            enviorements.Add(se1);
            enviorements.Add(se2);



            File.WriteAllText(CONFIGURATION_FILE,JsonConvert.SerializeObject(enviorements, Formatting.Indented));
            Console.ReadLine();
        }
    }
}
