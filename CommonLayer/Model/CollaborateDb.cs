namespace FundooCommonLayer.Model
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class CollaborateDb
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [ForeignKey("Notes")]
    public int NoteID { get; set; }
    [ForeignKey("Users")]
    public int UserId { get; set; }
  }
}
