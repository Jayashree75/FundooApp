using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Services
{
  public class AdminBusiness : IAdminBusiness
  {
    private readonly IAdminRepository _adminRepository;
    public AdminBusiness(IAdminRepository adminRepository)
    {
      _adminRepository = adminRepository;
    }
    public ResponseModel AdminRegistration(Registratin registration)
    {
      if (registration != null)
      {
        return _adminRepository.AdminRegistration(registration);
      }
      else
      {
        return null;
      }
    }
    public List<GetAllUserResponse> GetAllUser(string keyword)
    {    
        return _adminRepository.GetAllUser(keyword);
    }

    public ResponseModel Login(Login login)
    {
      if (login != null)
      {
        return _adminRepository.Login(login);
      }
      else
      {
        return null;
      }
    }
    public Dictionary<string, int> Statistics(int userid)
    {
        return _adminRepository.Statistics(userid);
    }
  }
}
