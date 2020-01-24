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

    public async Task<LabelModel> AddLabels(RequestedLabel requestedLabel,int userid)
    {
      LabelModel label = new LabelModel
      {
        LabelName = requestedLabel.LabelName,
        IsCreated = DateTime.Now,
        IsModified = DateTime.Now,
        UserId = userid
      };
      _userContext.label.Add(label);
      await _userContext.SaveChangesAsync();
      return label;
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

    public List<LabelModel> GetLabel(int userid)
    {
      List<LabelModel> labelModels = _userContext.label.Where(a => a.UserId == userid).ToList();
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
