using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public LabelModel AddLabels(string labelName, int userid)
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

    public bool DeleteLabel(int labelid)
    {
      LabelModel labelModel = _userContext.label.FirstOrDefault(c => c.LabelID == labelid);
      if(labelModel != null)
      {
        _userContext.label.Remove(labelModel);
        this._userContext.SaveChanges();
        return true;
      }
      else
      {
        return false;
      }
    }

    public LabelModel UpdateLabels(RequestedLabel requestedLabel, int userid,int labelid)
    {
      LabelModel labelModel = _userContext.label.FirstOrDefault(c =>(c.UserId == userid)&&(c.LabelID==labelid));
      var label = new LabelModel();
      labelModel.LabelID = requestedLabel.LabelId;
      labelModel.LabelName = requestedLabel.LabelName;
      labelModel.IsModified = DateTime.Now;
      labelModel.IsCreated = DateTime.Now;
      var label1 = this._userContext.label.Attach(labelModel);
      label1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      this._userContext.SaveChanges();
      return labelModel;
    }
  }
}
