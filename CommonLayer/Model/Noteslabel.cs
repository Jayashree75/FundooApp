using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
  public class Noteslabel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [ForeignKey("Notes")]
    public int NoteID { get; set; }
    [ForeignKey("label")]
    public int LabelID { get; set; }
  }
}
