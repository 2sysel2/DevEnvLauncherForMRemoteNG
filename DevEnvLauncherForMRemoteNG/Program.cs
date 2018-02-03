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
        private static readonly string CONFIGURATION_FILE = "configurations.json";

        static void Main(string[] args)
        {
            if(!File.Exists(CONFIGURATION_FILE))
            {
                createSampleConfig();
                return;
            }

            IList<DevelopmentEnviorement> enviorements = JsonConvert.DeserializeObject <List<DevelopmentEnviorement>>(File.ReadAllText(CONFIGURATION_FILE));

            bool failureDetected = false;
            foreach(DevelopmentEnviorement enviorement in enviorements)
            {
                if (!enviorement.IsEnviorement("Clarks"))
                {
                    if (!enviorement.Start())
                    {
                        failureDetected = true;
                    }
                }
            }

            if (failureDetected)
            {
                Console.ReadLine();
            }
        }

        static void createSampleConfig()
        {
            File.Create(CONFIGURATION_FILE).Close();

            Executable testExecutable = new Executable("C:\\path\\to\\executable", "params");
            List<DevelopmentEnviorement> enviorements = new List<DevelopmentEnviorement>();
            DevelopmentEnviorement se1 = new DevelopmentEnviorement("sampleConfig1");
            se1.Configurations.Add(testExecutable);
            se1.Configurations.Add(testExecutable);
            DevelopmentEnviorement se2 = new DevelopmentEnviorement("sampleConfig1");
            se2.Configurations.Add(testExecutable);
            se2.Configurations.Add(testExecutable);

            enviorements.Add(se1);
            enviorements.Add(se2);



            File.WriteAllText(CONFIGURATION_FILE,JsonConvert.SerializeObject(enviorements, Formatting.Indented));
        }
    }
}
