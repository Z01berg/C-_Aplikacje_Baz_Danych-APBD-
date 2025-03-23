using Xunit;
using LegacyApp;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void FirstName_Null_ReturnsFalse()
        {
            var userService = new UserService();
            var result = userService.AddUser(null, "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
            Assert.False(result);
        }
        
        [Fact]
        public void LastName_Null_ReturnsFalse()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", null, "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
            Assert.False(result);
        }

        [Fact]
        public void Email_NoSymbol_ReturnsFalse()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
            Assert.False(result);
        }

        [Fact]
        public void UnderageUser_ReturnsFalse()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2020-03-21"), 1);
            Assert.False(result);
        }
    }
}
