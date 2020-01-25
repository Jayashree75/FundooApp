using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interfaces
{
  public interface ILabelRepository
  {
    Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel, int userid);
    Task<LabelModel> UpdateLabels(RequestedLabel requestedLabel, int userid,int labelid);
    Task<bool> DeleteLabel(int labelid);
    List<LabelResponseModel> GetLabel(int userid);
  }
}
