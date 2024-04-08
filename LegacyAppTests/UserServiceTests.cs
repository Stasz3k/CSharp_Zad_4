namespace LegacyApp.Tests;

[TestFixture]
public class UserServiceTests
{

    [Test]
    public void AddUser_Invalid_FirstName_ReturnsFalse()
    {
        var userService = new UserService();
        
        var firstName = userService.AddUser("", "Doe", "test@example.com", new DateTime(1990, 1, 1), 1);
        
        Assert.IsFalse(firstName);
    }

    [Test]
    public void AddUser_Invalid_LastName_ReturnsFalse()
    {
        var userService = new UserService();
        var lastName = userService.AddUser("John", "", "test@example.com", new DateTime(1980, 1, 1), 1);
        
        Assert.IsFalse(lastName);
    }

    [Test]
    public void AddUser_Invalid_Email_ReturnsFalse()
    {
        var userService = new UserService();

        var result = userService.AddUser("John", "Doe", "invalid_email", new DateTime(1990, 1, 1), 1);
        
        Assert.IsFalse(result);
    }

    [Test]
    public void AddUser_TooYoung_ReturnsFalse()
    {
        var userService = new UserService();
        
        var result = userService.AddUser("John", "Doe", "test@example.com", DateTime.Now.AddYears(-20), 1);
        
        Assert.IsFalse(result);
    }



    [Test]
    public void IsValid_Input_ReturnsTrue()
    {
        var userService = new UserService();

        var isValidInput = userService.AddUser("John", "Doe", "test@example.com", new DateTime(1990, 1, 1), 1);
           
        Assert.IsTrue(isValidInput);
    }
    

}
