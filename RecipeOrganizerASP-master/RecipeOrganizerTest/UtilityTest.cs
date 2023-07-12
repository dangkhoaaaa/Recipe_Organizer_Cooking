using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Utilities;

namespace RecipeOrganizerTest
{
    //cần làm thêm nuget test coverage

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
		[Test, Category("LastLoginTest")]
        // attribute indicates a method is a test method.
        public void LastLoginNowTest_ShouldReturnMessageNow()
        {
            //Arrange
            const string expected = "Just now"; 
            
            //Act
            var result = MyToys.getLastTime(DateTime.Now);
            
            //Assert
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
        [Test, Category("LastLoginTest")]
        public void LastLogin4Seconds_ShouldReturnMessageLast4Seconds()
        {
			//Arrange
			DateTime currentDateTime = DateTime.Now;
			DateTime fourSecondsAgo = currentDateTime.AddSeconds(-4);
			const string expected = "4 seconds ago";

			//Act
			var result = MyToys.getLastTime(fourSecondsAgo);

			//Assert
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
        [Test, Category("LastLoginTest")]
        // attribute indicates a method is a test method.
        public void LastLogin5Minutes_ShouldReturnMessageLast5Minutes()
        {
			//Arrange
			DateTime currentDateTime = DateTime.Now;
            DateTime fiveMinutesAgo = currentDateTime.AddMinutes(-5);
            const string expected = "5 minutes ago";

			//Act
			var result = MyToys.getLastTime(fiveMinutesAgo);

			//Assert
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
        [Test, Category("LastLoginTest")]
        // attribute indicates a method is a test method.
        public void LastLogin6Hours_ShouldReturnMessageLast6Hours()
        {
			//Arrange
			DateTime currentDateTime = DateTime.Now;
            DateTime fourMinutesAgo = currentDateTime.AddHours(-6);
            const string expected = "6 hours ago";

			//Act
			var result = MyToys.getLastTime(fourMinutesAgo);

			//Assert
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
        [Test, Category("LastLoginTest")]
        // attribute indicates a method is a test method.
        public void LastLogin7Days_ShouldReturnMessageLast7Days()
        {
			//Arrange
			DateTime currentDateTime = DateTime.Now;
            DateTime sevenDaysAgo = currentDateTime.AddDays(-7);
            const string expected = "7 days ago";

			//Act
			var result = MyToys.getLastTime(sevenDaysAgo);

			//Assert
			Assert.AreEqual(expected, result);
        }

        [Test, Category("LastLoginMultipleTest")]
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
    



    //[TestFixture] 
    ////[Parallelizable(ParallelScope.All)] // Apply Parallelizable attribute at the fixture level
    //public class TestParallel1
    //{
    //	[Test]
    //	public void TestSleep1()
    //	{
    //		Thread.Sleep(3000);
    //	}
    //}

    //[TestFixture] 
    ////[Parallelizable(ParallelScope.All)] // Apply Parallelizable attribute at the fixture level
    //public class TestParallel2
    //{
    //	[Test]
    //	public void TestSleep2()
    //	{
    //		Thread.Sleep(3000);
    //	}
    //}

}
