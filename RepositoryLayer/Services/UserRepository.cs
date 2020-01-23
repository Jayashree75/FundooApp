//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
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

  /// <summary>
  /// This is the class of UserRepository.
  /// </summary>
  /// <seealso cref="FundooRepositoryLayer.Interfaces.IUserRepository" />
  public class UserRepository : IUserRepository
  {
    private readonly UserContext _userContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotesRepository"/> class.
    /// </summary>
    /// <param name="userContext">The user context.</param>
    public UserRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    /// <summary>
    /// Method for Forget the password.
    /// </summary>
    /// <param name="forgetPassword">The forget password.</param>
    /// <returns></returns>
    public ResponseModel ForgetPassword(ForgetPassword forgetPassword)
    {
      try
      {
        var data = _userContext.Users.FirstOrDefault(user => user.Email == forgetPassword.Email);
        if (data != null)
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// method for Logins the specified login.
    /// </summary>
    /// <param name="login">The login.</param>
    /// <returns>return userdata if successful</returns>
    public ResponseModel Login(Login login)
    {
      try
      {
        login.Password = EncodeDecode.EncodePassword(login.Password);
        var data = _userContext.Users.FirstOrDefault(user => (user.Email == login.Email) && (user.Password == login.Password));
        if (data != null)
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// method for Registers the specified user details.
    /// </summary>
    /// <param name="userDetails">The user details.</param>
    /// <returns>returns userdetails</returns>
    public UserDetails Register(Registratin registratin)
    {
      try
      {
        registratin.Password = EncodeDecode.EncodePassword(registratin.Password);
        var model = new UserDetails()
        {
          FirstName = registratin.FirstName,
          LastName = registratin.LastName,
          Email = registratin.Email,
          Password = registratin.Password,
          Type = registratin.Type,
          IsActive = true,
          IsCreated=DateTime.Now,
          IsModified=DateTime.Now
        };
        _userContext.Users.Add(model);
        _userContext.SaveChanges();
        return model;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// method for Resets the password.
    /// </summary>
    /// <param name="resetPassword">The reset password.</param>
    /// <returns>it return true if password changed sucessfully</returns>
  public bool ResetPassword(ResetPassword resetPassword)
    {
      try
      {
        UserDetails userDetails = _userContext.Users.FirstOrDefault(user => user.UserId == resetPassword.UserId);
        if (userDetails != null)
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}
