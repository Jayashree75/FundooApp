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

  public class AdminRepository : IAdminRepository
  {
    private readonly UserContext _userContext;

    public AdminRepository(UserContext userContext)
    {
      _userContext = userContext;
    }
    public ResponseModel AdminRegistration(Registratin registration)
    {
      registration.Password = EncodeDecode.EncodePassword(registration.Password);
      var model = new UserDetails()
      {
        FirstName = registration.FirstName,
        LastName = registration.LastName,
        Email = registration.Email,
        Password = registration.Password,
        Type = "Advance",
        UserRole = "Admin",
        IsActive = true,
        IsCreated = DateTime.Now,
        IsModified = DateTime.Now,
      };
      _userContext.Add(model);
      _userContext.SaveChanges();
      ResponseModel responseModel = new ResponseModel()
      {
        UserId = model.UserId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        IsActive = model.IsActive,
        IsCreated = model.IsCreated,
        IsModified = model.IsModified,
        Type = model.Type,
        UserRole = model.UserRole
      };
      return responseModel;
    }

    public List<GetAllUserResponse> GetAllUser(int pagenumber,int pagesize,string keyword)
    {
      List<GetAllUserResponse> getAllUserResponses = _userContext.Users.Where(Linq => Linq.UserRole == "user").Select(
        linq => new GetAllUserResponse
        {
          userid = linq.UserId,
          FirstName = linq.FirstName,
          LastName = linq.LastName,
          Email = linq.Email,
          Type = linq.Type,
          UserRole = linq.UserRole
        }).ToList();
      foreach (var get in getAllUserResponses)
      {
        get.NumberOfNotes = _userContext.Notes.Where(linq => linq.UserId == get.userid).Count();
      }
      if (keyword != null)
      {
        List<GetAllUserResponse> noteResponseModels = SearchEmail(keyword);
        return getAllUserResponses;
      }
      int count = getAllUserResponses.Count();
      int currentpage = pagenumber;
      int sizeofpage = pagesize;
      int totalpages = (int)Math.Ceiling(count / (double)sizeofpage);
      if(currentpage==0)
      {
        currentpage++;
        var items = getAllUserResponses.Skip(currentpage - 1 * sizeofpage).Take(pagesize).ToList();
      }
      int numberofObjectperpage = pagesize;
      var result = getAllUserResponses.Skip(numberofObjectperpage * pagenumber).Take(numberofObjectperpage);
      return result.ToList();
    }

    /// <summary>
    /// Searches the note.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="keyword">The keyword.</param>
    /// <returns></returns>
    public List<GetAllUserResponse> SearchEmail(string keyword)
    {
      List<GetAllUserResponse> userResponseModels = null;
      if (keyword != null)
      {
        userResponseModels = _userContext.Users.Where(linq => (linq.Email.Contains(keyword))).
          Select(linq => new GetAllUserResponse
          {
            userid=linq.UserId,
            Email=linq.Email,
            FirstName=linq.FirstName,
            LastName=linq.LastName,
            Type=linq.Type,
           UserRole=linq.UserRole,         
          }
        ).ToList();
      }
      return userResponseModels;
    }
    public ResponseModel Login(Login login)
    {
      login.Password = EncodeDecode.EncodePassword(login.Password);
      var data = _userContext.Users.FirstOrDefault(linq => linq.Email == login.Email && linq.Password == login.Password);
      if (data != null)
      {
        var admindata = new ResponseModel()
        {
          FirstName = data.FirstName,
          LastName = data.LastName,
          Email = data.Email,
          Type = data.Type,
          IsCreated = data.IsCreated,
          IsModified = data.IsModified,
          UserRole = data.UserRole
        };
        return admindata;
      }
      else
      {
        return null;
      }
    }
    public Dictionary<string, int> Statistics(int userid)
    {
      int basic = 0;
      int advance = 0;
      Dictionary<string, int> dist = new Dictionary<string, int>();
      basic = _userContext.Users.Where(linq => (linq.UserRole == "user") && (linq.Type == "Basic")).Count();
      advance = _userContext.Users.Where(linq => (linq.UserRole == "user") && (linq.Type == "Advance")).Count();

      dist.Add("Basic", basic);
      dist.Add("Advance", advance);
      return dist;
    }
  }
}
