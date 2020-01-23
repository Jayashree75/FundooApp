using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public string Color { get; set; }
  }

}
