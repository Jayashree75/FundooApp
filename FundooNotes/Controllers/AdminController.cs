namespace FundooNotes.Controllers
{
  using System;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Configuration;
  using Microsoft.IdentityModel.Tokens;


  [Route("api/[controller]")]
  [ApiController]

  [EnableCors("CorsPolicy")]
  public class AdminController : ControllerBase
  {
    private readonly IAdminBusiness _adminBusiness;
    private IConfiguration _config;

    public AdminController(IAdminBusiness adminBusiness, IConfiguration config)
    {
      _adminBusiness = adminBusiness;
      _config = config;
    }
    [HttpPost]
    public IActionResult Registration(Registratin registration)
    {
      var result = _adminBusiness.AdminRegistration(registration);
      if (result != null)
      {
        var success = true;
        var message = "Registration is successfully done";
        return Ok(new { success, message, result });
      }
      else
      {
        var success = false;
        var message = "Registration Failed";
        return BadRequest(new { success, message });
      }
    }
    [HttpPost]
    [Route("AdminLogin")]
    public IActionResult Login(Login login)
    {
      var result = _adminBusiness.Login(login);
      if (result != null)
      {

        var token = GenerateJSONWebToken(result, "Login");
        var success = true;
        var message = "Login successfully done";
        return Ok(new { success, message, result, token });
      }
      else
      {
        var success = false;
        var message = "Login Failed";
        return BadRequest(new { success, message });
      }
    }
    [HttpGet]
    public IActionResult Statistics()
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "UserRole").Value == "Admin")
          {
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var result = _adminBusiness.Statistics(UserId);
            if (result != null)
            {
              status = true;
              message = "Statistics";
              return Ok(new { status, message, result });
            }
            else
            {
              status = false;
              message = "statistics failed";
              return NotFound(new { status, message });
            }
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpGet]
    [Route("AllUser")]
    public IActionResult GetAllUser(string keyword)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "UserRole").Value == "Admin")
          {
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var result = _adminBusiness.GetAllUser(keyword);
            if (result != null)
            {
              status = true;
              message = "List of All User";
              return Ok(new { status, message, result });
            }
            else
            {
              status = false;
              message = "getting List of user has been failed";
              return NotFound(new { status, message });
            }
          }
        }
      }
      return BadRequest("used invalid token");
    }
    private string GenerateJSONWebToken(ResponseModel responseModel, string type)
    {
      try
      {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
        new Claim("UserId",responseModel.UserId.ToString()),
        new Claim("Email",responseModel.Email.ToString()),
        new Claim("Typetoken",type),
        new Claim("UserRole",responseModel.UserRole)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          claims,
          expires: DateTime.Now.AddDays(1),
          signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}