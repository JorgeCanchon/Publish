using System;
using System.Diagnostics;

namespace PublishV1
{
    public class RunScript
    {
        private string _routeScript;
        public RunScript(string routeScript)
        {
            _routeScript = routeScript;
        }
        public void Run()
        {
            Console.WriteLine($"Publicación iniciada {DateTime.Now}");
            Process.Start(_routeScript);
        }
    }
}


