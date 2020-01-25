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
    public async Task<IActionResult> AddNotes([FromBody] RequestedNotes requestedNotes)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      NoteResponseModel notesDB = new NoteResponseModel();
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
           int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDB = await _notesBusiness.AddNotes(requestedNotes,UserId);
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
    [HttpPut]
    [Route("{noteid}")]
    public async Task<IActionResult> UpdateNotes([FromBody] RequestedNotes requestedNotes, int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          NoteResponseModel noteResponseModel = await _notesBusiness.UpdateNotes(requestedNotes, noteid, UserId);
          if (requestedNotes != null)
          {
            status = true;
            message = "Notes are Updated";
            return Ok(new { status, message, requestedNotes});
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

    [HttpGet]
    public IActionResult GetNotes()
    {
      List<NoteResponseModel> notesDBs = new List<NoteResponseModel>();
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

    [HttpGet]
    [Route("{noteid}")]
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
          NoteResponseModel noteResponseModel = _notesBusiness.GetNotesByNoteId(noteid, userId);
          if (noteResponseModel != null)
          {
            status = true;
            message = "Notes all data";
            return Ok(new { status, message, noteResponseModel });
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

    [HttpDelete("{noteid}")]
    public async Task<IActionResult> DeleteNotes(int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = await _notesBusiness.DeleteNotes(noteid);
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

    [HttpPost]
    [Route("Trash/{noteid}")]
    public async Task<IActionResult> TrashNotes(int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result =await _notesBusiness.Trash(UserId, noteid);
          if (result)
          {
            status = true;
            message = "Note trashed";
            return Ok(new { status, message });
          }
          else
          {
            status = false;
            message = "Note trash failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    [HttpGet]
    [Route("Trashed")]
    public IActionResult GetTrashedList()
    {
      List<NoteResponseModel> notesDBs = new List<NoteResponseModel>();
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDBs = _notesBusiness.GetAllTrashed(UserId);
          if (notesDBs != null)
          {
            status = true;
            message = "All Trashed Notes";
            return Ok(new { status, message, notesDBs });
          }
          else
          {
            status = false;
            message = "Trashed Notes are Empty";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    [HttpPost]
    [Route("Archive/{noteid}")]
    public async Task<IActionResult> ArchiveNotes(int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = await _notesBusiness.Archive(UserId, noteid);
          if (result)
          {
            status = true;
            message = "Note Archive";
            return Ok(new { status, message });
          }
          else
          {
            status = false;
            message = "Note Archive failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    [HttpGet]
    [Route("Archive")]
    public IActionResult GetArchiveList()
    {
      List<NoteResponseModel> notesDBs = new List<NoteResponseModel>();
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDBs = _notesBusiness.GetAllArchive(UserId);
          if (notesDBs != null)
          {
            status = true;
            message = "All Archive Notes";
            return Ok(new { status, message, notesDBs });
          }
          else
          {
            status = false;
            message = "Archive Notes are Empty";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    [HttpPost]
    [Route("Pinned/{noteid}")]
    public async Task<IActionResult> PinnedNotes( int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = await _notesBusiness.Pinned(UserId, noteid);
          if (result)
          {
            status = true;
            message = "pinned";
            return Ok(new { status, message });
          }
          else
          {
            status = false;
            message = "Pinned failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    [HttpGet]
    [Route("Pinned")]
    public IActionResult GetPinnedList()
    {
      List<NoteResponseModel> notesDBs = new List<NoteResponseModel>();
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDBs = _notesBusiness.GetAllPinned(UserId);
          if (notesDBs != null)
          {
            status = true;
            message = "All Pinned Notes";
            return Ok(new { status, message, notesDBs });
          }
          else
          {
            status = false;
            message = "Unpinned notes";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }    
  }
}