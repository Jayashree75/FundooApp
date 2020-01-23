namespace FundooNotes.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class LabelController : ControllerBase
  {
    private readonly ILabelBusiness _labelBusiness;

    public LabelController(ILabelBusiness labelBusiness)
    {
      _labelBusiness = labelBusiness;
    }
    [HttpPost]
    [Route("AddLabels")]
    public IActionResult AddLabels(string labels)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          var labelModel = _labelBusiness.AddLabels(labels, userid);
          if (labelModel != null)
          {
            status = true;
            message = "label added successfully";
            return Ok(new { status, message, labelModel });
          }
          else
          {
            status = false;
            message = "label added failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpPut]
    [Route("UpdateLabel")]
    public IActionResult UpdateLabels(RequestedLabel requestedlabel)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          var labelModel = _labelBusiness.UpdateLabels(requestedlabel, userid, requestedlabel.LabelId);
          if (labelModel != null)
          {
            status = true;
            message = "label Updated successfully";
            return Ok(new { status, message, labelModel });
          }
          else
          {
            status = false;
            message = "label Updation failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpDelete]
    public IActionResult DeleteLabels(int labelid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          var labelModel = _labelBusiness.DeleteLabel(labelid);
          if (labelModel != false)
          {
            status = true;
            message = "label Deleted successfully";
            return Ok(new { status, message, labelModel });
          }
          else
          {
            status = false;
            message = "label Deletion failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
  }
}
