namespace FundooBusinessLayer.Interfaces
{
  using CommonLayer.Model;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;


  public interface IUserBusiness
  {
    UserDetails Register(UserDetails userDetails);
    ResponseModel Login(Login login);
    ResponseModel ForgetPassword(ForgetPassword forgetPassword);
    bool ResetPassword(ResetPassword resetPassword);
  }
}
