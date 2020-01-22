namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
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
    public LabelModel AddLabels(string labels, long userid)
    {
      if (string.IsNullOrWhiteSpace(labels) || userid <= 0)
        return null;
      else
        return _labelRepository.AddLabels(labels, userid);
    }

    public LabelModel UpdateLabels(string labels, long userid)
    {
      throw new NotImplementedException();
    }
  }
}
