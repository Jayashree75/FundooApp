using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Model;
using FundooBusinessLayer.Interfaces;
using FundooBusinessLayer.Services;
using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FundooNotes.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserAccountController : ControllerBase
  {
    private readonly IUserBusiness _userBusiness;
       private IConfiguration _config;

    public UserAccountController(IUserBusiness userBusiness, IConfiguration config)
    {
      this._userBusiness = userBusiness;
      _config = config;
    }
 
    [HttpPost]
    public IActionResult Registration([FromBody] UserDetails userDetails)
    {
      var result = _userBusiness.Register(userDetails);
      if (result != null)
      {
        var sucess = true;
        var message = "Registration successful";
        return Ok(new {sucess,message});
      }
      else
      {
        var sucess = true;
        var message = "Registration failed";
        return BadRequest(new { sucess, message });
      }
    }
    [HttpPost]
    [Route("Login")]
    public IActionResult Login(Login login)
    {
      ResponseModel data = _userBusiness.Login(login);
      if (data!=null)
      {
        var token = GenerateJSONWebToken(data);
        var success = true;
        var message = "Login successful";
        return Ok(new { success, message });
      }
      else
      {
        var success = false;
        var message = "Login Failed";
        return BadRequest(new { success, message });
      }
    }
    private string GenerateJSONWebToken(ResponseModel responseModel)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new[] {
      new Claim("UserId",responseModel.UserId.ToString()),
      new Claim("Email",responseModel.Email.ToString())
    };
      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        null,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }  
  }
}
