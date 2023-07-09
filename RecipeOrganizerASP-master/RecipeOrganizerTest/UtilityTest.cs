using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Utilities;

namespace RecipeOrganizerTest
{
    [TestFixture] //attribute denotes a class that contains unit tests
    //[Parallelizable(ParallelScope.All)] // Apply Parallelizable attribute at the fixture level
    public class Test1
    {
        [Test]
        public void TestSleep1() { 
            Thread.Sleep(2000);
        }
    }

    [TestFixture] //attribute denotes a class that contains unit tests
    //[Parallelizable(ParallelScope.All)] // Apply Parallelizable attribute at the fixture level
    public class Test2
    {
        [Test]
        public void TestSleep2()
        {
            Thread.Sleep(2000);
        }
    }

    public static class TestDataGenerator
    {
        public static IEnumerable<TestCaseData> GetLastLoginTestData()
        {
            DateTime currentDateTime = DateTime.Now;

            yield return new TestCaseData(DateTime.Now, "Just now");
            yield return new TestCaseData(currentDateTime.AddSeconds(-4), "4 seconds ago");
            yield return new TestCaseData(currentDateTime.AddMinutes(-5), "5 minutes ago");
            yield return new TestCaseData(currentDateTime.AddHours(-6), "6 hours ago");
            yield return new TestCaseData(currentDateTime.AddDays(-7), "7 days ago");
        }
    }

	[TestFixture]
	public class MyDataDrivenTests
	{
		[TestCaseSource(typeof(TestDataGenerator), nameof(TestDataGenerator.GetLastLoginTestData))]
		public void MultiplyLastLoginTime_ShouldReturnCorrectMessage(DateTime lastTime, string expectedMessage)
		{
			// Arrange

			// Act
			var result = MyToys.getLastTime(lastTime);

			// Assert
			Assert.AreEqual(expectedMessage, result);
		}
	}


	[TestFixture] // attribute denotes a class that contains unit tests
    public class UtilityTest
    {
        

        //Test Last Login calculate function
        //Test case #1: Check user last login to Recipe Organizer app
        //Procedure: 
        //  1. User login app
        //  2. Given the time to MyToys.getLastTime
        //  3. Execute MyToys.getLastTime
        //  4. Return message
        //Expected value
        //      Given DateTime.Now must be return message 'Just now'
        //      Expected value = 'Just now'
        [Test]
        // attribute indicates a method is a test method.
        public void LastLoginNowTestReturnMessageNow()
        {
            const string expected = "Just now";
            var result = MyToys.getLastTime(DateTime.Now);
            Assert.AreEqual(expected, result);
        }

        //Test Last Login calculate function
        //Test case #2: Check user last login to Recipe Organizer app
        //Procedure: 
        //  1. User login app
        //  2. Given the time to MyToys.getLastTime()
        //  3. Execute MyToys.getLastTime()
        //  4. Return message
        //Expected value
        //      Given the last 4 seconds last login must be return message '4 seconds ago'
        //      Expected value = '4 seconds ago'
        [Test]
        // attribute indicates a method is a test method.
        public void LastLogin4SecondsReturnMessageLast4Seconds()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime fourSecondsAgo = currentDateTime.AddSeconds(-4);

            const string expected = "4 seconds ago";
            var result = MyToys.getLastTime(fourSecondsAgo);

            Assert.AreEqual(expected, result);
        }

        //Test Last Login calculate function
        //Test case #3: Check user last login to Recipe Organizer app
        //Procedure: 
        //  1. User login app
        //  2. Given the time to MyToys.getLastTime()
        //  3. Execute MyToys.getLastTime()
        //  4. Return message
        //Expected value
        //      Given the last 5 minute last login must be return message '5 minutes ago'
        //      Expected value = '5 minutes ago'
        [Test]
        // attribute indicates a method is a test method.
        public void LastLogin5MinutesReturnMessageLast5Minutes()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime fiveMinutesAgo = currentDateTime.AddMinutes(-5);

            const string expected = "5 minutes ago";
            var result = MyToys.getLastTime(fiveMinutesAgo);

            Assert.AreEqual(expected, result);
        }

        //Test Last Login calculate function
        //Test case #4: Check user last login to Recipe Organizer app
        //Procedure: 
        //  1. User login app
        //  2. Given the time to MyToys.getLastTime()
        //  3. Execute MyToys.getLastTime()
        //  4. Return message
        //Expected value
        //      Given the last 6 hours last login must be return message '6 hours ago'
        //      Expected value = '6 hours ago'
        [Test]
        // attribute indicates a method is a test method.
        public void LastLogin4MinuteReturnMessageLast4Minute()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime fourMinutesAgo = currentDateTime.AddHours(-6);

            const string expected = "6 hours ago";
            var result = MyToys.getLastTime(fourMinutesAgo);

            Assert.AreEqual(expected, result);
        }

        //Test Last Login calculate function
        //Test case #5: Check user last login to Recipe Organizer app
        //Procedure: 
        //  1. User login app
        //  2. Given the time to MyToys.getLastTime()
        //  3. Execute MyToys.getLastTime()
        //  4. Return message
        //Expected value
        //      Given the last 7 days last login must be return message '7 days ago'
        //      Expected value = '7 days ago'
        [Test]
        // attribute indicates a method is a test method.
        public void LastLogin7DaysReturnMessageLast7Days()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime sevenDaysAgo = currentDateTime.AddDays(-7);

            const string expected = "7 days ago";
            var result = MyToys.getLastTime(sevenDaysAgo);

            Assert.AreEqual(expected, result);
        }

        //tương ứng với các input
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = MyToys.IsPrime(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}
