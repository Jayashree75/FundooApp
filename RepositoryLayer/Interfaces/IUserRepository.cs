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
    /// <summary>
    /// Registers the specified registratin.
    /// </summary>
    /// <param name="registratin">The registratin.</param>
    /// <returns></returns>
    Task<ResponseModel> Register(Registratin registratin);

    /// <summary>
    /// Logins the specified login.
    /// </summary>
    /// <param name="login">The login.</param>
    /// <returns></returns>
    ResponseModel Login(Login login);

    /// <summary>
    /// Forgets the password.
    /// </summary>
    /// <param name="forgetPassword">The forget password.</param>
    /// <returns></returns>
    ResponseModel ForgetPassword(ForgetPassword forgetPassword);

    /// <summary>
    /// Resets the password.
    /// </summary>
    /// <param name="resetPassword">The reset password.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    Task<bool> ResetPassword(ResetPassword resetPassword, int userid);
  }
}
