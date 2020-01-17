using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.ModelRequest
{
  public class Login
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
  public class ForgetPassword
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}