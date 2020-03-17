namespace FundooRepositoryLayer.Interfaces
{
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface IAdminRepository
  {
    ResponseModel AdminRegistration(Registratin registration);
    ResponseModel Login(Login login);
    Dictionary<string, int> Statistics(int userid);
    List<GetAllUserResponse> GetAllUser(string keyword);
  }
}
