using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.ModelRequest
{
  public class RequestedNotes
  { 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Reminder { get; set; }
    public string Image { get; set; }
    [DefaultValue(false)]
    public bool IsArchive { get; set; }
    [DefaultValue(false)]
    public bool IsPin { get; set; }
    public bool IsTrash { get; set; }
    public string Color { get; set; }
    public List<RequestNotesLabel> labels { get; set; }

  }
  public class RequestNotesLabel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LabelID { get; set; }
  }
}
