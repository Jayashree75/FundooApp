//-----------------------------------------------------------------------
// <copyright file="Login.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.ModelRequest
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

  /// <summary>
  /// this the class for initializing login data.
  /// </summary>
  public class Login
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }

  /// <summary>
  /// this the class for initializing ForgetPassword data.
  /// </summary>
  public class ForgetPassword
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }

  /// <summary>
  /// this the class for initializing ResetPassword data.
  /// </summary>
  public class ResetPassword
  {
    public int UserId { get; set; }
    public string Password { get; set; }
  }
  public class Registratin
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; set; }
  }
}