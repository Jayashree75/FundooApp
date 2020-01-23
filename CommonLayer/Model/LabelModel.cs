namespace FundooCommonLayer.Model
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Text;


  public class LabelModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LabelID { get; set; }
    public string LabelName { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }
    public int NoteID { get; set; }
    [ForeignKey("Users")]
    public int UserId { get; set; }
  }
}
