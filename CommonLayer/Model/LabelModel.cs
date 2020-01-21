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
    public int LabelID{ get; set; }
    public string LabelName { get; set; }
    public bool IsCreated { get; set; }
    public bool IsModified { get; set; }


  }
}
