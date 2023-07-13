using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecipeOrganizerTest
{
    public class MockTest 
    {
        [TestFixture]
        public class UserServiceTests
        {
            [Test]
            public void IsUserAdmin_UserIsAdmin_ReturnsTrue()
            {
                // Arrange
                int userId = 1;
                var mockRepository = new Mock<IUsersRepository>();
                mockRepository.Setup(r => r.GetUserById(userId)).Returns(new Services.Repository.User { Id = userId, IsAdmin = true ,Name ="joi" });
                var service = new UserService(mockRepository.Object);

                // Act
                bool result = service.IsUserAdmin(userId);

                // Assert
                Assert.IsTrue(result);
                mockRepository.Verify(r => r.GetUserById(userId), Times.Once);
            }

            [Test]
            public void IsUserAdmin_UserNotAdmin_ReturnsFalse()
            {
                // Arrange
                int userId = 2;
                var mockRepository = new Mock<IUsersRepository>();
                mockRepository.Setup(r => r.GetUserById(userId)).Returns(new Services.Repository.User { Id = userId, IsAdmin = false , Name="kk" });
                var service = new UserService(mockRepository.Object);

                // Act
                bool result = service.IsUserAdmin(userId);

                // Assert
                Assert.IsFalse(result);
                mockRepository.Verify(r => r.GetUserById(userId), Times.Once);
            }
        }
    }
}
