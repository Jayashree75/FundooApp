namespace FundooBusinessLayer.Services
{
  using CommonLayer.Model;
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using System;
  public class UserBusiness : IUserBusiness
  {
    private readonly IUserRepository _userRepository;
    public UserBusiness(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public ResponseModel ForgetPassword(ForgetPassword forgetPassword)
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

    public ResponseModel Login(Login login)
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

    public UserDetails Register(UserDetails userDetails)
    {
      if (userDetails != null)
      {
        return _userRepository.Register(userDetails);
      }
      else
      {
        throw new Exception("User Details is Empty");
      }
    }
    public bool ResetPassword(ResetPassword resetPassword)
    {
      if((resetPassword.UserId==0) || (resetPassword.Password==null))
      {
        return false;
      }
      else
      {
        return _userRepository.ResetPassword(resetPassword);
      }
    }
  }
}
