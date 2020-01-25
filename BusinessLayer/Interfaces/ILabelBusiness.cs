namespace FundooBusinessLayer.Interfaces
{
    using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
    using System;
  using System.Collections.Generic;
  using System.Text;
    using System.Threading.Tasks;

    public interface ILabelBusiness
  {
    Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel, int userid);
    Task<LabelModel> UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid);
    Task<bool> DeleteLabel(int labelid);
    List<LabelResponseModel> GetLabel(int userid);

  }
}
