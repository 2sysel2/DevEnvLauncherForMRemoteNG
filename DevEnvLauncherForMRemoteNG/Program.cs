﻿using Newtonsoft.Json;
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

            if (args.Length == 0)
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

            IList<DevelopmentEnviorement> enviorements = new List<DevelopmentEnviorement>();
            try
            {
                string json = File.ReadAllText(CONFIGURATION_FILE);
                foreach (DevelopmentEnviorement de in JsonConvert.DeserializeObject<List<DevelopmentEnviorement>>(json))
                {
                    enviorements.Add(de);
                }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("JSON configuration is not valid.");
                Console.WriteLine(e.Message);
            }
            IList<StartResultDto> results = new List<StartResultDto>();
            foreach (DevelopmentEnviorement enviorement in enviorements)
            {
                if (configurationsToLaunch.Contains(enviorement.Name))
                {
                    results.Add(enviorement.Start());
                }
            }
            bool waitForUserInput = false;

            foreach (StartResultDto result in results)
            {
                if (result.debugExecutables.Count > 0 || result.failedExecutables.Count > 0)
                {
                    waitForUserInput = true;
                }
            }


            if (waitForUserInput) {
                Console.ReadLine();
            }
            
        }

        static void createSampleConfig()
        {
            Console.WriteLine("Creating sample config [{0}]",CONFIGURATION_FILE);
            File.Create(CONFIGURATION_FILE).Close();

            List<DevelopmentEnviorement> enviorements = new List<DevelopmentEnviorement>();
            DevelopmentEnviorement se1 = new DevelopmentEnviorement("sampleConfig1");
            se1.Configurations.Add(new Executable("C:\\path\\to\\executable1", "params",false));
            se1.Configurations.Add(new Executable("C:\\path\\to\\executable2", "params",false));
            DevelopmentEnviorement se2 = new DevelopmentEnviorement("sampleConfig2");
            se2.Configurations.Add(new Executable("C:\\path\\to\\executable3", "params",false));
            se2.Configurations.Add(new Executable("C:\\path\\to\\executable4", "params",false));

            enviorements.Add(se1);
            enviorements.Add(se2);



            File.WriteAllText(CONFIGURATION_FILE,JsonConvert.SerializeObject(enviorements, Formatting.Indented));
            Console.ReadLine();
        }
    }
}
