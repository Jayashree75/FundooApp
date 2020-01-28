//-----------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
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

  /// <summary>
  /// This is controller for label.
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
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

    /// <summary>
    /// Add the label.
    /// </summary>
    /// <param name="requestedlabel">The requestedlabel.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<IActionResult> AddLabels(RequestedLabel requestedlabel)
    {
      try
      {
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
          {
            int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var labelModel = await _labelBusiness.AddLabels(requestedlabel, userid);
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Update the label.
    /// </summary>
    /// <param name="requestedlabel">The requestedlabel.</param>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPut]
    [Route("{labelid}")]
    public async Task<IActionResult> UpdateLabels(RequestedLabel requestedlabel,int labelid)
    {
      try
      {
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
          {
            int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var labelModel = await _labelBusiness.UpdateLabels(requestedlabel, userid, labelid);
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Deletes the label.
    /// </summary>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{labelid}")]
    public async Task<IActionResult> DeleteLabels(int labelid)
    {
      try
      {
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
          {
            int userid = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var labelModel = await _labelBusiness.DeleteLabel(labelid);
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets all label.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    public IActionResult GetAllLabel()
    {
      try
      {
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
          {
            int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var labelmodel = _labelBusiness.GetLabel(userId);
            if (labelmodel != null)
            {
              status = true;
              message = "All label";
              return Ok(new { status, message, labelmodel });
            }
            else
            {
              status = false;
              message = "not a valid label id";
              return NotFound(new { status, message });
            }
          }
        }
        return BadRequest("used invalid token");
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}