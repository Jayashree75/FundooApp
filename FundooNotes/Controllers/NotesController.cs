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
    [Route("AddNotes")]
    public IActionResult AddNotes([FromBody] RequestedNotes requestedNotes)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      NotesDB notesDB = new NotesDB();
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
           int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDB = _notesBusiness.AddNotes(requestedNotes,UserId);
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
    [Route("GetNotes")]
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
    [Route("Delete/{id}")]
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
    [HttpPost]
    [Route("Trash")]
    public IActionResult TrashNotes(int userid, int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = _notesBusiness.Trash(UserId, noteid);
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
    [Route("AllTrash")]
    public IActionResult GetTrashedList()
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
    [Route("Archive")]
    public IActionResult ArchiveNotes(int userid, int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = _notesBusiness.Archive(UserId, noteid);
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
    [Route("AllArchive")]
    public IActionResult GetArchiveList()
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
    [Route("Pinned")]
    public IActionResult PinnedNotes(int userid, int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          bool result = _notesBusiness.Pinned(UserId, noteid);
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
    [Route("AllPinned")]
    public IActionResult GetPinnedList()
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

    [HttpGet]
    [Route("GetBy/{id}")]
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