namespace FundooBusinessLayer.Services
{
  using CommonLayer.Model;
  using FundooBusinessLayer.Interfaces;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
    using System;
  using System.Collections.Generic;
  using System.Text;


  public class UserBusiness : IUserBusiness
  {
    private readonly IUserRepository _userRepository;
    public UserBusiness(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public ResponseModel Login(Login login)
    {
    
      if(string.IsNullOrWhiteSpace(login.Email)|| string.IsNullOrWhiteSpace(login.Password))
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

  }
}
