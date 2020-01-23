//-----------------------------------------------------------------------
// <copyright file="UserAccountController.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
  using System;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using CommonLayer.Model;
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooCommonLayer.MSMQ;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Configuration;
  using Microsoft.IdentityModel.Tokens;


  /// <summary>
  /// This is the controller class.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class UserAccountController : ControllerBase
  {
    private readonly IUserBusiness _userBusiness;
    private IConfiguration _config;

    public UserAccountController(IUserBusiness userBusiness, IConfiguration config)
    {
      _userBusiness = userBusiness;
      _config = config;
    }

    /// <summary>
    /// This is the method for Post Registration.
    /// </summary>
    /// <param name="userDetails"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Registration")]
    public IActionResult Registration([FromBody] Registratin registratin)
    {
      try
      {
        var result = _userBusiness.Register(registratin);
        if (result != null)
        {
          var sucess = true;
          var message = "Registration successful";
          return Ok(new { sucess, message });
        }
        else
        {
          var sucess = true;
          var message = "Registration failed";
          return BadRequest(new { sucess, message });
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for Post the loginDetail.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Login")]
    public IActionResult Login(Login login)
    {
      try
      {
        ResponseModel data = _userBusiness.Login(login);
        if (data != null)
        {
          var token = GenerateJSONWebToken(data, "Login");
          var success = true;
          var message = "Login successful";
          return Ok(new { success, message, data, token });
        }
        else
        {
          var success = false;
          var message = "Email is not registered";
          return BadRequest(new { success, message });
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the httppost method for forgetPassword.
    /// </summary>
    /// <param name="forgetPassword"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Forget")]
    public IActionResult ForgetPassword(ForgetPassword forgetPassword)
    {
      ResponseModel data = _userBusiness.ForgetPassword(forgetPassword);
      if (data != null)
      {
        var token = GenerateJSONWebToken(data, "ForgetPassword");
        var success = true;
        var message = "Email Verified";
        Send.SendMSMQ(token,forgetPassword.Email);
        return Ok(new { success, message, token });
      }
      else
      {
        var success = false;
        var message = "Email not matched";
        return Ok(new { success, message });
      }
    }

    /// <summary>
    /// This is the method for ResetPassword.
    /// </summary>
    /// <param name="resetPassword"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [Route("Reset")]
    public IActionResult ResetPassword(ResetPassword resetPassword)
    {
      try
      {
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "ForgetPassword")
          {
            resetPassword.UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            status = _userBusiness.ResetPassword(resetPassword);
            if (status)
            {
              status = true;
              message = "Your password has been successfully changed";
              return Ok(new { status, message });
            }
          }
        }
        status = false;
        message = "Invalid User";
        return NotFound(new { status, message });
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for GenerateToken.
    /// </summary>
    /// <param name="responseModel"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GenerateJSONWebToken(ResponseModel responseModel, string type)
    {
      try
      {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
        new Claim("UserId",responseModel.UserId.ToString()),
        new Claim("Email",responseModel.Email.ToString()),
        new Claim("Typetoken",type)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          claims,
          expires: DateTime.Now.AddMinutes(30),
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
