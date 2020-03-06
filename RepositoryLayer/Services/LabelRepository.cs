//-----------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Services
{
using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  /// <summary>
  /// This is the class of labelRepository.
  /// </summary>
  /// <seealso cref="FundooRepositoryLayer.Interfaces.ILabelRepository" />
  public class LabelRepository : ILabelRepository
  {
    private readonly UserContext _userContext;
    public LabelRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    /// <summary>
    /// Adds the labels.
    /// </summary>
    /// <param name="requestedLabel">The requested label.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel,int userid)
    {
      try
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
        LabelResponseModel labelResponseModel = new LabelResponseModel()
        {
          LabelID = label.LabelID,
          LabelName = label.LabelName,
          IsCreated = label.IsCreated,
          IsModified = label.IsModified
        };      
        return labelResponseModel;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Deletes the label.
    /// </summary>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteLabel(int labelid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the label.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<LabelResponseModel> GetLabel(int userid)
    {
      try
      {
        List<LabelResponseModel> labelModels = _userContext.label.Where(a => a.UserId == userid)
            .Select(a => new LabelResponseModel
            {
              LabelID = a.LabelID,
              LabelName = a.LabelName,
              IsCreated = a.IsCreated,
              IsModified = a.IsModified
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Updates the labels.
    /// </summary>
    /// <param name="requestedLabel">The requested label.</param>
    /// <param name="userid">The userid.</param>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<LabelResponseModel> UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid)
    {
      try
      {
        LabelModel labelModel = _userContext.label.FirstOrDefault(c => (c.UserId == userid) && (c.LabelID == labelid));
        if (labelModel != null)
        {
          labelModel.LabelName = requestedLabel.LabelName;
          var note = this._userContext.label.Attach(labelModel);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
        }
        LabelResponseModel labelResponseModel = new LabelResponseModel()
        {
          LabelID = labelModel.LabelID,
          LabelName = labelModel.LabelName,
          IsCreated = labelModel.IsCreated,
          IsModified = labelModel.IsModified
        };
        return labelResponseModel;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}
