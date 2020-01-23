//-----------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
using System;
using System.Collections.Generic;
using System.Text;

  /// <summary>
  /// This is the class for responsemodel.
  /// </summary>
  public class ResponseModel
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
  }
}
