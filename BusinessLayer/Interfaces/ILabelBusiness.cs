//-----------------------------------------------------------------------
// <copyright file="ILabelBusiness.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Interfaces
{
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Threading.Tasks;

  /// <summary>
  /// This is the interface for label Business.
  /// </summary>
  public interface ILabelBusiness
  {
    /// <summary>
    /// Adds the labels.
    /// </summary>
    /// <param name="requestedLabel">The requested label.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    Task<LabelResponseModel> AddLabels(RequestedLabel requestedLabel, int userid);

    /// <summary>
    /// Updates the labels.
    /// </summary>
    /// <param name="requestedLabel">The requested label.</param>
    /// <param name="userid">The userid.</param>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    Task<LabelResponseModel> UpdateLabels(RequestedLabel requestedLabel, int userid, int labelid);

    /// <summary>
    /// Deletes the label.
    /// </summary>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    Task<bool> DeleteLabel(int labelid);

    /// <summary>
    /// Gets the label.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    List<LabelResponseModel> GetLabel(int userid);
  }
}
