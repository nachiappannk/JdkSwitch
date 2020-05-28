using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JdkSwitch
{
    class Program
    {
        const string ENVIRONMENT_VARIABLE_PREFIX = "JAVA_SDK_";
        const string ENVIRONMENT_VARIABLE_TO_SET = "JAVA_HOME";

        static IEnumerable<string> GetVariableNames()
        {
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                var key = de.Key.ToString();
                if (!key.StartsWith(ENVIRONMENT_VARIABLE_PREFIX)) continue;
                yield return de.Key.ToString().Replace(ENVIRONMENT_VARIABLE_PREFIX, "");
            }
        }
        static void Main(string[] args)
        {
            var variables = GetVariableNames().ToList();
            Console.WriteLine("Please select the version");
            Console.WriteLine("0\t:EXIT");
            for (int i = 0; i < variables.Count; i++)
            {
                Console.WriteLine($"{i + 1}\t:{variables[i]}");
            }

            var input = Console.ReadLine();
            var intInput = int.Parse(input);
            if (intInput == 0)
            {
                Console.WriteLine("Exiting the application due to user input");
                Environment.Exit(0);
            }

            var key = variables[intInput - 1];
            Console.WriteLine($"using {key}");
            var environmentVariableName = $"{ENVIRONMENT_VARIABLE_PREFIX}{key}";
            var environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Machine);
            Console.WriteLine($"Writing {environmentVariableValue}");
            Environment.SetEnvironmentVariable(ENVIRONMENT_VARIABLE_TO_SET, environmentVariableValue, EnvironmentVariableTarget.Machine);
            Console.WriteLine("Bye... Press any key");
            Console.ReadKey();

        }
    }
}
