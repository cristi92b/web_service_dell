using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    class Program
    {
        static void Main(string[] args)
        {
            long port = 8080;
            if (args.Length > 0)
            {
                string portNumberStr = args[0];
                Int64.TryParse(portNumberStr, out port);
            }
            EmailNameController emailName = new EmailNameController(port);
            emailName.StartListening();
            Console.ReadKey();
        }
    }
}
