using FundooCommonLayer.Model;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Services
{
  public class LabelRepository : ILabelRepository
  {
    private readonly UserContext _userContext;
    public LabelRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    public LabelModel AddLabels(string labelName, long userid)
    {
      LabelModel label = new LabelModel
      {
        LabelName = labelName,
        IsCreated = DateTime.Now,
        IsModified = DateTime.Now,
        UserId = userid
      };
      _userContext.label.Add(label);
      _userContext.SaveChanges();
      return label;
    }

    public LabelModel UpdateLabels(string labels, long userid)
    {
      throw new NotImplementedException();
    }
  }
}
