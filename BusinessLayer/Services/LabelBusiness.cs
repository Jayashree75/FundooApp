namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;
    using System.Threading.Tasks;

    public class LabelBusiness : ILabelBusiness
  {
    private readonly ILabelRepository _labelRepository;
    public LabelBusiness(ILabelRepository labelRepository)
    {
      _labelRepository = labelRepository;
    }
    public async Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel, int userid)
    {
      if (requestedLabel!=null || userid <= 0)
        return null;
      else
        return await _labelRepository.AddLabels(requestedLabel, userid);
    }

    public async Task<bool> DeleteLabel(int labelid)
    {
      if(labelid!=0)
      {
        return await _labelRepository.DeleteLabel(labelid);
      }
      else
      {
        return false;
      }
    }

    public List<LabelResponseModel> GetLabel(int userid)
    {
      if(userid!=0)
      {
        return _labelRepository.GetLabel(userid);
      }
      else
      {
        return null;
      }
    }

    public async Task<LabelModel> UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid)
    {
      if (userid != 0)
      {
        return await _labelRepository.UpdateLabels(requestedLabel, userid,labelid);
      }
      else
      {
        return null;
      }
    }
  }
}
