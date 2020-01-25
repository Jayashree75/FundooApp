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
    using System.Threading.Tasks;

    /// <summary>
    /// this is an interface of BusinessLayer.
    /// </summary>
    public interface IUserBusiness
  {
    Task<ResponseModel> Register(Registratin registratin);
    ResponseModel Login(Login login);
    ResponseModel ForgetPassword(ForgetPassword forgetPassword);
    Task<bool> ResetPassword(ResetPassword resetPassword,int userid);
  }
}
