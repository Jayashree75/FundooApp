using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Interfaces
{
 public interface IAdminBusiness
  {
     ResponseModel AdminRegistration(Registratin registration);
     ResponseModel Login(Login login);
    Dictionary<string, int> Statistics(int userid);
    List<GetAllUserResponse> GetAllUser(string keyword);
  }
}
