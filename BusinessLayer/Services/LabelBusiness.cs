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

  /// <summary>
  /// This is the class for LabelBusiness.
  /// </summary>
  /// <seealso cref="FundooBusinessLayer.Interfaces.ILabelBusiness" />
  public class LabelBusiness : ILabelBusiness
  {
    private readonly ILabelRepository _labelRepository;
    public LabelBusiness(ILabelRepository labelRepository)
    {
      _labelRepository = labelRepository;
    }

    /// <summary>
    /// Adds the labels.
    /// </summary>
    /// <param name="requestedLabel">The requested label.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel, int userid)
    {
      try
      {
        if (requestedLabel != null || userid <= 0)
          return await _labelRepository.AddLabels(requestedLabel, userid);
        else
          return null;
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
        if (labelid != 0)
        {
          return await _labelRepository.DeleteLabel(labelid);
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
        if (userid != 0)
        {
          return _labelRepository.GetLabel(userid);
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
        if (userid != 0)
        {
          return await _labelRepository.UpdateLabels(requestedLabel, userid, labelid);
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
  }
}
