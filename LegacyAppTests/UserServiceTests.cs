namespace LegacyApp.Tests;

[TestFixture]
public class UserServiceTests
{
    private UserService _userService;
    
    [SetUp]
    public void SetUp()
    {
        _userService = new UserService();
    }
    
    
    [Test]
    public void AddUser_Invalid_FirstName_ReturnsFalse()
    {
        var firstName = _userService.AddUser("", "Doe", "test@example.com", new DateTime(1990, 1, 1), 1);
        
        Assert.IsFalse(firstName);
    }

    [Test]
    public void AddUser_Invalid_LastName_ReturnsFalse()
    {
        var lastName = _userService.AddUser("John", "", "test@example.com", new DateTime(1980, 1, 1), 1);
        
        Assert.IsFalse(lastName);
    }

    [Test]
    public void AddUser_Invalid_Email_ReturnsFalse()
    {
        var result = _userService.AddUser("John", "Doe", "invalid_email", new DateTime(1990, 1, 1), 1);
        
        Assert.IsFalse(result);
    }

    [Test]
    public void AddUser_TooYoung_ReturnsFalse()
    {
        var result = _userService.AddUser("John", "Doe", "test@example.com", DateTime.Now.AddYears(-20), 1);
        
        Assert.IsFalse(result);
    }



    [Test]
    public void IsValid_Input_ReturnsTrue()
    {
        var isValidInput = _userService.AddUser("John", "Doe", "test@example.com", new DateTime(1990, 1, 1), 1);
           
        Assert.IsTrue(isValidInput);
    }
    

}
