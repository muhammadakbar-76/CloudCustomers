using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
                new User()
                {
                    Id = 1,
                    Name = "Muhammad Akbar",
                    Email = "muhammadakbar509@gmail.com",
                    Address = new Address()
                    {
                        City = "Kotabaru",
                        Street = "Biduri",
                        ZipCode = "72116",
                    }
                },
                 new User()
                {
                    Id = 2,
                    Name = "Muhammad Udin",
                    Email = "muhammadudin@gmail.com",
                    Address = new Address()
                    {
                        City = "Kotabaru",
                        Street = "Rampa Baru",
                        ZipCode = "72116",
                    }
                },
                 new User()
                {
                    Id = 3,
                    Name = "Magfirah",
                    Email = "magfirah@gmail.com",
                    Address = new Address()
                    {
                        City = "Kotabaru",
                        Street = "Selokayang",
                        ZipCode = "72116",
                    }
                },
        };
    }
}
