//-----------------------------------------------------------------------
// <copyright file="IUserBusiness.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Interfaces
{
  using CommonLayer.Model;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;

  /// <summary>
  /// this is an interface of BusinessLayer.
  /// </summary>
  public interface IUserBusiness
  {
    UserDetails Register(UserDetails userDetails);
    ResponseModel Login(Login login);
    ResponseModel ForgetPassword(ForgetPassword forgetPassword);
    bool ResetPassword(ResetPassword resetPassword);
  }
}
