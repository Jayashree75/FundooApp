namespace FundooCommonLayer.ModelRequest
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;


  public class RequestedNotesUpdate
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Reminder { get; set; }
    public string Image { get; set; }
    [DefaultValue(false)]
    public bool IsArchive { get; set; }
    [DefaultValue(false)]
    public bool IsPin { get; set; }
    public bool IsTrash { get; set; }
    public string Color { get; set; }
    public List<RequestNotesLabel> labels { get; set; }
    public List<CollaborateRequest> collaborateRequests { get; set; }

  }
  public class RequestNotes
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Reminder { get; set; }
    public string Image { get; set; }
    [DefaultValue(false)]
    public bool IsArchive { get; set; }
    [DefaultValue(false)]
    public bool IsPin { get; set; }
    public bool IsTrash { get; set; }
    public string Color { get; set; }
  }
  public class RequestNotesLabel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LabelID { get; set; }
  }
  public class TrashValue
  {
    public bool Value { get; set; }
  }
  public class RequestColour
  {
    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    public string Color { get; set; }
  }
  public class ImageUpload
  {
    public string Image { get; set; }
  }
  public class CollaborateRequest
  {
    public int UserId { get; set; }
  }
  public class MultipleCollaborate
  {
    public List<CollaborateRequest> Collaborates { get; set; }
  }
}
