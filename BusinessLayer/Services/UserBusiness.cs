//-----------------------------------------------------------------------
// <copyright file="UserBusiness.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Services
{
  using CommonLayer.Model;
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using FundooRepositoryLayer.Services;
  using System;
  using System.Threading.Tasks;
    using Xunit.Sdk;

    /// <summary>
    /// This is the class of UserBusiness.
    /// </summary>
    public class UserBusiness : IUserBusiness
  {
    private readonly IUserRepository _userRepository;
    /// <summary>
    /// Initializes the new instance of UserBusiness.
    /// </summary>
    /// <param name="userRepository"></param>
    public UserBusiness(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    /// <summary>
    /// This is the method for forgetPassword.
    /// </summary>
    /// <param name="forgetPassword"></param>
    /// <returns></returns>
    public ResponseModel ForgetPassword(ForgetPassword forgetPassword)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(forgetPassword.Email))
        {
          return null;
        }
        else
        {
          return _userRepository.ForgetPassword(forgetPassword);
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }


    /// <summary>
    /// This is the method for Login.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public ResponseModel Login(Login login)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
        {
          return null;
        }
        else
        {
          return _userRepository.Login(login);
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Method for Register the user.
    /// </summary>
    /// <param name="userDetails"></param>
    /// <returns></returns>

    public async Task<ResponseModel> Register(Registratin registratin)
    {
      try
      {
        if (registratin != null)
        {
          return await _userRepository.Register(registratin);
        }
        else
        {
          throw new Exception("User Details is Empty");
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
    /// <summary>
    /// Method for reset the password.
    /// </summary>
    /// <param name="resetPassword"></param>
    /// <returns></returns>
    public async Task<bool> ResetPassword(ResetPassword resetPassword,int userid)
    {
      try
      {
        if (resetPassword.Password != null || userid!=0)
        {
          return await _userRepository.ResetPassword(resetPassword,userid);        
        }
        else
        {
          return false;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}