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
    [Required]
    public string FirstName { get; set; }
    [RegularExpression("^[a-zA-Z][a-zA-Z\\s]+$")]
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
    public string UserRole { get; set; }
    public string ProfilePicture { get; set; }
  }
}
