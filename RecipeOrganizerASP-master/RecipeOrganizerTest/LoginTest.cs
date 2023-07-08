using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Utilities;

namespace RecipeOrganizerTest
{
    [TestFixture]
    // attribute denotes a class that contains unit tests
    public class LoginTest
    {
        [Test]
        // attribute indicates a method is a test method.
        public void LoginTest_COM1_Returns1()
        {
            const string expected = "Just now";
            var result = MyToys.getLastTime(DateTime.Now);
            Assert.AreEqual(expected, result);
            
        }
    }
}
