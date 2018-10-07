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
            long port = 16359;
            EmailNameController emailName = new EmailNameController(port);
            emailName.StartListening();
            Console.ReadKey();
        }
    }
}
