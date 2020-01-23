namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;


  public class LabelBusiness : ILabelBusiness
  {
    private readonly ILabelRepository _labelRepository;
    public LabelBusiness(ILabelRepository labelRepository)
    {
      _labelRepository = labelRepository;
    }
    public LabelModel AddLabels(string labels, int userid)
    {
      if (string.IsNullOrWhiteSpace(labels) || userid <= 0)
        return null;
      else
        return _labelRepository.AddLabels(labels, userid);
    }

    public bool DeleteLabel(int labelid)
    {
      if(labelid!=0)
      {
        return _labelRepository.DeleteLabel(labelid);
      }
      else
      {
        return false;
      }
    }

    public LabelModel UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid)
    {
      if (userid != 0)
      {
        return _labelRepository.UpdateLabels(requestedLabel, userid,labelid);
      }
      else
      {
        return null;
      }
    }
  }
}
