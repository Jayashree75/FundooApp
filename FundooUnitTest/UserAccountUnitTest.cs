using FundooBusinessLayer.Interfaces;
using FundooBusinessLayer.Services;
using FundooCommonLayer.ModelRequest;
using FundooNotes.Controllers;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelDB;
using FundooRepositoryLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FundooUnitTest
{
  public class UserAccountUnitTest
  {

    private readonly IUserRepository _userRepository;
    private readonly IUserBusiness _userBusiness;
    private readonly IConfiguration _config;

    public static DbContextOptions<UserContext> DbContext { get; }
    public static string sqlConnection = "Data Source=.;Initial Catalog=FundooDB;Integrated Security=True";

    static UserAccountUnitTest()
    {
      DbContext = new DbContextOptionsBuilder<UserContext>().UseSqlServer(sqlConnection).Options;
    }

    public UserAccountUnitTest()
    {
      var context = new UserContext(DbContext);
      _userRepository = new UserRepository(context);
      _userBusiness = new UserBusiness(_userRepository);

      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
      configurationBuilder.AddJsonFile("appsettings.json");
      _config = configurationBuilder.Build();
    }

    [Fact]
    public void Task_LoginUser_Return_OkResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = "sawakarejayashree@gmail.com",
        Password = "123456789"
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<OkObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_Login_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = " ",
        Password = "123456789"
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_LoginPassword_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = "sawakarejayashree@gmail.com",
        Password = ""
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_LoginEmailPassword_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = "",
        Password = ""
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_LoginEmailPasswordvalidate_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = "fgdfhff",
        Password = "354yrthtgfhhf"
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_Loginok_Return_OkResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Login
      {
        Email = "sawakarejayashree@gmail.com",
        Password = "jhfhfdhg"
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_ForgetPasswoed_Return_OkResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new ForgetPassword
      {
       Email="jayusawakare14@gmail.com" 
      };
      var expecteddata = controller.ForgetPassword(data);
      Assert.IsType<OkObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_ForgetPasswodEmptyEmail_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new ForgetPassword
      {
        Email = ""
      };
      var expecteddata = controller.ForgetPassword(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_ForgetPasswodWrongEmail_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new ForgetPassword
      {
        Email = "21325344"
      };
      var expecteddata = controller.ForgetPassword(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_ForgetPasswodEmailvalidate_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new ForgetPassword
      {
        Email = "jayusawakare14gmail.com"
      };
      var expecteddata = controller.ForgetPassword(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_ForgetPasswodEmailwithoutdot_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new ForgetPassword
      {
        Email = "jayusawakare14@gmailcom"
      };
      var expecteddata = controller.ForgetPassword(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
    [Fact]
    public void Task_Registration_Return_BadResult()
    {
      var controller = new UserAccountController(_userBusiness, _config);
      var data = new Registratin
      {
        FirstName="",
        LastName="",
        Email = "",
        Password="",
        Type=""
      };
      var expecteddata = controller.Registration(data);
      Assert.IsType<BadRequestObjectResult>(expecteddata);
    }
  }
}
