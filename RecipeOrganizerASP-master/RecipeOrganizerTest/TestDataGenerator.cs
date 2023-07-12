using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeOrganizerTest
{
    public static class TestDataGenerator
    {
        public static IEnumerable<TestCaseData> GetLastLoginTestData()
        {
            DateTime currentDateTime = DateTime.Now;

            yield return new TestCaseData(currentDateTime.AddMinutes(-5), "5 minutes ago");
            yield return new TestCaseData(currentDateTime.AddHours(-6), "6 hours ago");
            yield return new TestCaseData(currentDateTime.AddDays(-7), "7 days ago");
        }
    }
}
