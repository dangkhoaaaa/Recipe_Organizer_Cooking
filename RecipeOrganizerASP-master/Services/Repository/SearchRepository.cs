using Microsoft.EntityFrameworkCore;
using Services.Models.Authentication;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IUsersRepository
    {
        public User GetUserById(int userId);
    }

    public class UsersRepository : IUsersRepository
    {
        public User GetUserById(int userId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin người dùng theo Id.

            return new User { Id = userId, Name = "John Smith", IsAdmin = true };
        }
    }

    public class UserService
    {
        private IUsersRepository _userRepository;

        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
            User uses = new User()
            {
                Id = 1,
                IsAdmin = true,
                Name = "Join"
            };
        }

        public bool IsUserAdmin(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return user != null && user.IsAdmin;
        }
    }

    public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsAdmin { get; set; }
        }
    }

