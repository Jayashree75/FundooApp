namespace FundooCommonLayer.Model
{
    using FundooCommonLayer.ModelRequest;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class NotesDB
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NoteID { get; set; }
    [ForeignKey("Users")]
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Reminder { get; set; }
    public string Image { get; set; }
    [DefaultValue(false)]
    public bool IsArchive { get; set; }
    [DefaultValue(false)]
    public bool IsPin { get; set; }
    [DefaultValue(false)]
    public bool IsTrash { get; set; } 
    public string Color { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set;}
  }
}
