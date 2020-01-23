//-----------------------------------------------------------------------
// <copyright file="UserDetails.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace CommonLayer.Model
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  /// <summary>
  /// this is the class for userdetails.
  /// </summary>
  public class UserDetails
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    [RegularExpression("^[a-zA-Z][a-zA-Z\\s]+$")]
    public string FirstName { get; set; }
    [RegularExpression("^[a-zA-Z][a-zA-Z\\s]+$")]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
  }
}
