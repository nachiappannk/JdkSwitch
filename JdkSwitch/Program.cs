using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JdkSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("config.json");
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            var path = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Machine);

            Console.WriteLine("The current path is " + path);

            foreach (var kv in dictionary)
            {
                var item = kv.Value + "\\bin";
                path = path.Replace(item + ";", "");
                path = path.Replace(item, "");
            }

            Console.WriteLine("Please select the version");
            Console.WriteLine("0\t:EXIT");
            var keys = dictionary.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                Console.WriteLine($"{i + 1}\t:{keys[i]}");
            }

            var input = Console.ReadLine();
            var intInput = int.Parse(input);

            if (intInput == 0)
            {
                Console.WriteLine("Exiting the application due to user input");
                Environment.Exit(0);
            }

            var key = keys[intInput - 1];

            var value = dictionary[key];
            path = $"{value.TrimEnd('\\')}\\bin;{path}";
            Console.WriteLine($"using {value}");

            Environment.SetEnvironmentVariable("JAVA_HOME", value, EnvironmentVariableTarget.Machine);

            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Machine);

            Console.WriteLine("Bye... Press any key");
            Console.ReadKey();

        }
    }
}
