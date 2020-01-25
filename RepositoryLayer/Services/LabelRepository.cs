using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Services
{
  public class LabelRepository : ILabelRepository
  {
    private readonly UserContext _userContext;
    public LabelRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    public async Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel,int userid)
    {
      LabelModel label = new LabelModel
      {
        LabelName = requestedLabel.LabelName,
        IsCreated = DateTime.Now,
        IsModified = DateTime.Now,
        UserId = userid
      };
      LabelResponseModel labelResponseModel = new LabelResponseModel()
      {
        LabelID = label.LabelID,
        LabelName = label.LabelName,
        IsCreated = label.IsCreated,
        IsModified = label.IsModified
      };
      _userContext.label.Add(label);
      await _userContext.SaveChangesAsync();
      return labelResponseModel;
    }

   

    public async Task<bool> DeleteLabel(int labelid)
    {
      LabelModel labelModel = _userContext.label.FirstOrDefault(c => c.LabelID == labelid);
      if (labelModel != null)
      {
        _userContext.label.Remove(labelModel);
        await this._userContext.SaveChangesAsync();
        return true;
      }
      else
      {
        return false;
      }
    }

    public List<LabelResponseModel> GetLabel(int userid)
    {
      List<LabelResponseModel> labelModels = _userContext.label.Where(a => a.UserId == userid)
        .Select(a=>new LabelResponseModel
        {
         LabelID=a.LabelID,
         LabelName=a.LabelName,
         IsCreated=a.IsCreated,
         IsModified=a.IsModified
        }).ToList();
      if (labelModels != null)
      {
        return labelModels;
      }
      else
      {
        return null;
      }
    }

    public async Task<LabelModel> UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid)
    {
      LabelModel labelModel = _userContext.label.FirstOrDefault(c => (c.UserId == userid) && (c.LabelID == labelid));
      var label = new LabelModel();
      labelModel.LabelID = labelid;
      labelModel.LabelName = requestedLabel.LabelName;
      labelModel.IsModified = DateTime.Now;
      labelModel.IsCreated = DateTime.Now;
      var label1 = this._userContext.label.Attach(labelModel);
      label1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      await this._userContext.SaveChangesAsync();
      return labelModel;
    }
  }
}
