using System;
using System.Threading.Tasks;
using System.Configuration;

namespace PublishV1
{
    class Program
    {
        static void Main(string[] args)
        {
            string date = ConfigurationManager.AppSettings.Get("DatetimePublish");
            DateTime start = Convert.ToDateTime(date.ToString());
            Console.WriteLine($"Fecha de la publicación: {start}");
            Parallel.Invoke(() => {
                while (true)
                {
                    RunPublish(start);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }
            });
        }
        static void RunPublish(DateTime start)
        {
            try
            {
                DateTime end = Convert.ToDateTime(DateTime.Now.ToString());
                int result = DateTime.Compare(start, end);
                if (result < 0)
                {
                    Console.WriteLine($"Fecha no válida {start}");
                    Task.Delay(TimeSpan.FromMinutes(0.5)).Wait();
                    Environment.Exit(-1);
                }

                if (result == 0)
                {
                    string route = ConfigurationManager.AppSettings.Get("Route");
                    RunScript rs = new RunScript(route.ToString());
                    rs.Run();
                    Task.Delay(TimeSpan.FromMinutes(0.5)).Wait();
                    Environment.Exit(-1);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp.Message} - {exp.StackTrace}");
                Task.Delay(TimeSpan.FromMinutes(0.5)).Wait();
                Environment.Exit(-1);
            }
        }
    }
}
