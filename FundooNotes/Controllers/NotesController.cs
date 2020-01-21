﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class NotesController : ControllerBase
  {
    private readonly INotesBusiness _notesBusiness;

    public NotesController(INotesBusiness notesBusiness)
    {
      _notesBusiness = notesBusiness;
    }

    [HttpPost]
    [Route("AddNotes")]
    public IActionResult AddNotes([FromBody] NotesDB notesDB)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          notesDB.UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDB = _notesBusiness.AddNotes(notesDB);
          if (notesDB != null)
          {
            status = true;
            message = "Notes added successfully";
            return Ok(new { status, message, notesDB });
          }
          else
          {
            status = false;
            message = "Note added failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpGet]
    [Route("GetAllNotes")]
    public IActionResult GetNotes()
    {
      List<NotesDB> notesDBs = new List<NotesDB>();
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDBs = _notesBusiness.GetNotes(UserId);
          if (notesDBs != null)
          {
            status = true;
            message = "All Notes";
            return Ok(new { status, message, notesDBs });
          }
          else
          {
            status = false;
            message = "Notes are Empty";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpPut]
    [Route("UpdateNotes")]
    public IActionResult UpdateNotes([FromBody] NotesDB notesDB)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          notesDB.UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDB = _notesBusiness.UpdateNotes(notesDB);
          if (notesDB != null)
          {
            status = true;
            message = "Notes are Updated";
            return Ok(new { status, message, notesDB });
          }
          else
          {
            status = false;
            message = "Notes updation failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpDelete]
    [Route("DeleteNote")]
    public IActionResult DeleteNotes(int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = _notesBusiness.DeleteNotes(noteid);
          if (result)
          {
            status = true;
            message = "Notes are Deleted";
            return Ok(new { status, message });
          }
          else
          {
            status = false;
            message = "Notes deletion failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
    [HttpGet]
    [Route("GetNotesByNotesId/{id}")]
    public IActionResult GetNotesByNotesId(int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          NotesDB notesDB = _notesBusiness.GetNotesByNoteId(noteid, userId);
          if (notesDB != null)
          {
            status = true;
            message = "Notes all data";
            return Ok(new { status, message, notesDB });
          }
          else
          {
            status = false;
            message = "Note is not present";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }
  }
}

