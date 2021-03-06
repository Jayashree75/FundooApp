﻿namespace FundooCommonLayer.Model
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

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
