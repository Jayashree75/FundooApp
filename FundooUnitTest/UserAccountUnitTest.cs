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
using System;
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
        Email = "jayusawakare14@gmail.com",
        Password = "123456789"
      };
      var expecteddata = controller.Login(data);
      Assert.IsType<OkObjectResult>(expecteddata);
    }
  }
}
