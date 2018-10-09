using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Tests
{
    [TestClass()]
    public class EmailNameControllerTests
    {
        [TestMethod()]
        public void EmailNameControllerTest()
        {

        }

        [TestMethod()]
        public void StartListeningTest()
        {
            EmailNameController controller = new EmailNameController(8080);
            controller.StartListening();

        }
    }
}