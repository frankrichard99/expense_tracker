using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class UserService
    {
        static private List<User> users = new List<User> { new User { Id = 1, FirstName = "frank", LastName = "richard", Email="rich@yahoo.com", Age = 19 }, new User { Id = 2, FirstName = "chibuike", LastName = "john", Email = "john@yahoo.com",  Age = 23 }, new User { Id = 3, FirstName = "benny", LastName = "kiur", Email = "benny@yahoo.com", Age = 13 } };

        public List<User> GetUsers() {
            return users;
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }
    }
}
