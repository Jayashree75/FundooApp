﻿//-----------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// This is the class for responsemodel.
  /// </summary>
  public class ResponseModel
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Priofilepic { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
    public string UserRole { get; set; }
  }
  public class NoteResponseModel
  {
    public int NoteID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Reminder { get; set; }
    public string Image { get; set; }
    public bool IsArchive { get; set; }
    public bool IsPin { get; set; }
    public bool IsTrash { get; set; }
    public string Color { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
    public List<LabelResponseModel> labels { get; set; }
    public List<CollaborateResponse> CollaborateResponse { get; set; }
  }
  public class LabelResponseModel
  {
    public int LabelID { get; set; }
    public string LabelName { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
  }
  public class CollaborateResponse
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }
  public class GetAllUserResponse
  {
    public int userid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public string UserRole { get; set; }
    public int NumberOfNotes {  get; set; }
  }
}
