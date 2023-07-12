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
            yield return new TestCaseData(DateTime.Now, "Just now");
            yield return new TestCaseData(DateTime.Now.AddSeconds(-4), "4 seconds ago");
            yield return new TestCaseData(DateTime.Now.AddMinutes(-5), "5 minutes ago");
            yield return new TestCaseData(DateTime.Now.AddHours(-6), "6 hours ago");
            yield return new TestCaseData(DateTime.Now.AddDays(-7), "7 days ago");
        }
    }
}
