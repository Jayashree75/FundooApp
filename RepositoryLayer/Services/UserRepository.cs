namespace FundooRepositoryLayer.Services
{
  using CommonLayer.Model;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using FundooRepositoryLayer.ModelDB;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  public class UserRepository : IUserRepository
  {
    private readonly UserContext _userContext;

    public UserRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    public ResponseModel ForgetPassword(ForgetPassword forgetPassword)
    {
      var data = _userContext.Users.FirstOrDefault(user => user.Email == forgetPassword.Email);
      if(data!=null)
      {
        var userdata = new ResponseModel()
        {
          UserId = data.UserId,
          FirstName = data.FirstName,
          LastName = data.LastName,
          Email = data.Email,
          Type = data.Type,
          IsActive = data.IsActive,
          IsCreated = data.IsCreated,
          IsModified = data.IsModified
        };
        return userdata;
      }
      return null;
    }
  
    public ResponseModel Login(Login login)
    {
      login.Password = EncodeDecode.EncodePassword(login.Password);
      var data = _userContext.Users.FirstOrDefault(user => (user.Email == login.Email) && (user.Password == login.Password));
      if (data!=null)
      {
        var userdata = new ResponseModel()
        {
          UserId = data.UserId,
          FirstName = data.FirstName,
          LastName = data.LastName,
          Email = data.Email,
          Type = data.Type,
          IsActive = data.IsActive,
          IsCreated=data.IsCreated,
          IsModified=data.IsModified
        };
        return userdata;
      }
      return null;
    }
    
    public UserDetails Register(UserDetails userDetails)
    {
      userDetails.Password= EncodeDecode.EncodePassword(userDetails.Password);
      userDetails.IsActive = true;
      userDetails.IsCreated = DateTime.Now;
      userDetails.IsModified = DateTime.Now;
      _userContext.Users.Add(userDetails);
      _userContext.SaveChanges();
      return userDetails;
    }

    bool IUserRepository.ResetPassword(ResetPassword resetPassword)
    {
      UserDetails userDetails = _userContext.Users.FirstOrDefault(user => user.UserId == resetPassword.UserId);
      if(userDetails!=null)
      {
        resetPassword.Password = EncodeDecode.EncodePassword(resetPassword.Password);
        userDetails.Password = resetPassword.Password;
        userDetails.IsModified = DateTime.Now;
        var user = _userContext.Users.Attach(userDetails);
        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _userContext.SaveChanges();
        return true;
      }
      return false;
    }
  }
}
