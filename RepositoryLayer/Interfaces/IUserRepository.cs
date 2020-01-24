//-----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
{
  using CommonLayer.Model;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This is an Interface of User Repository.
    /// </summary>
    public interface IUserRepository
  {
    Task<UserDetails> Register(Registratin registratin);
    ResponseModel Login(Login login);
    ResponseModel ForgetPassword(ForgetPassword forgetPassword);
    Task<bool> ResetPassword(ResetPassword resetPassword,int userid);
  }
}
