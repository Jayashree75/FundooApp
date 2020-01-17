using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.Model
{
  public class ResponseModel
  {
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime IsCreated { get; set; }
    public DateTime IsModified { get; set; }


  }
}
