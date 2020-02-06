//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz" Author="Jayashree sawakare">
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
  /// This is the notes controller class.
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
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

    /// <summary>
    /// Adds the notes.
    /// </summary>
    /// <param name="requestedNotes">The requested notes.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<IActionResult> AddNotes([FromBody] RequestedNotesUpdate requestedNotes)
    {
      try
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
            notesDB = await _notesBusiness.AddNotes(requestedNotes, UserId);
            if (notesDB != null)
            {
              status = true;
              message = "Note has been successfully added";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Updates the notes.
    /// </summary>
    /// <param name="requestedNotes">The requested notes.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPut]
    [Route("{noteid}")]
    public async Task<IActionResult> UpdateNotes([FromBody] RequestNotes requestedNotes, int noteid)
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            NoteResponseModel noteResponseModel = await _notesBusiness.UpdateNotes(requestedNotes, noteid, UserId);
            if (requestedNotes != null)
            {
              status = true;
              message = "Notes are Updated successfully";
              return Ok(new { status, message, requestedNotes });
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }


    /// <summary>
    /// Gets the notes.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    public IActionResult GetNotes(string keyword)
    {
      try
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
            notesDBs = _notesBusiness.GetNotes(UserId, keyword);
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }


    /// <summary>
    /// Gets the notes by notes identifier.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("{noteid}")]
    public IActionResult GetNotesByNotesId(int noteid)
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }


    /// <summary>
    /// Gets the notes by label identifier.
    /// </summary>
    /// <param name="labelid">The labelid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("{labelid}/Notes")]
    public IActionResult GetNotesByLabelId(int labelid)
    {
      try
      {
        List<NoteResponseModel> noteResponseModels = new List<NoteResponseModel>();
        bool status;
        string message;
        var user = HttpContext.User;
        if (user.HasClaim(c => c.Type == "Typetoken"))
        {
          if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
          {
            int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            noteResponseModels = _notesBusiness.GetNoteByLabelId(labelid);
            if (noteResponseModels != null)
            {
              status = true;
              message = "Notes all data by label id";
              return Ok(new { status, message, noteResponseModels });
            }
            else
            {
              status = false;
              message = "label is not present";
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
    /// Deletes the notes.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{noteid}")]
    public async Task<IActionResult> DeleteNotes(int noteid)
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            bool result = await _notesBusiness.DeleteNotes(noteid);
            if (result)
            {
              status = true;
              message = "Notes are Deleted successfully";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Trashes the notes.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPut]
    [Route("{noteid}/Trash")]
    public async Task<IActionResult> TrashNotes(int noteid,TrashValue trash)
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            bool result = await _notesBusiness.Trash(UserId, noteid,trash);
            if (result)
            {
              status = true;
              message = "Note trash successfully";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the trashed list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("Trash")]
    public IActionResult GetTrashedList()
    {
      try
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
              message = "List of All Trashed Notes";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Archives the notes.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPut]
    [Route("{noteid}/Archive")]
    public async Task<IActionResult> ArchiveNotes(int noteid,TrashValue archive)
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            bool result = await _notesBusiness.Archive(UserId, noteid,archive);
            if (result)
            {
              status = true;
              message = "Note Archive successfully done";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the archive list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("Archive")]
    public IActionResult GetArchiveList()
    {
      try
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
              message = "List of All Archive Notes";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Pinneds the notes.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPut]
    [Route("{noteid}/Pin")]
    public async Task<IActionResult> PinnedNotes(int noteid,TrashValue pin)
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            bool result = await _notesBusiness.Pinned(UserId, noteid,pin);
            if (result)
            {
              status = true;
              message = "Notes are pin successfully";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the pinned list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("Pin")]
    public IActionResult GetPinnedList()
    {
      try
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
              message = "List of All Pinned Notes";
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Deletes all trash.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete]
    public IActionResult DeleteAllTrash()
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
            int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            bool result = _notesBusiness.DeleteAllTrash(UserId);
            if (result)
            {
              status = true;
              message = "All Note Deleted";
              return Ok(new { status, message });
            }
            else
            {
              status = false;
              message = "Deletion failed";
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
    /// Colors the change.
    /// </summary>
    /// <param name="requestColour">The request colour.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{noteid}/Color")]
    public IActionResult ColorChange(RequestColour requestColour, int noteid)
    {
      bool status;
      string message;
      var user = HttpContext.User;
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          NoteResponseModel result = _notesBusiness.ColorChange(noteid, requestColour, UserId);
          if (result != null)
          {
            status = true;
            message = "Color Change successfully done";
            return Ok(new { status, message });
          }
          else
          {
            status = false;
            message = "Color change failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    /// <summary>
    /// Adds the image.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("{noteid}/Image")]
    public IActionResult AddImage(int noteid, [FromForm] ImageUpload image)
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
          string imageUrl = _notesBusiness.AddImage(noteid, UserId, image);
          if (imageUrl != null)
          {
            status = true;
            message = "Image added successfully";
            return Ok(new { status, message, imageUrl });
          }
          else
          {
            status = false;
            message = "Image added failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    /// <summary>
    /// Reminders the list.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Remainder")]
    public IActionResult ReminderList()
    {
      bool status;
      string message;
      var user = HttpContext.User;
      List<NoteResponseModel> notesDBs = new List<NoteResponseModel>();
      if (user.HasClaim(c => c.Type == "Typetoken"))
      {
        if (user.Claims.FirstOrDefault(c => c.Type == "Typetoken").Value == "Login")
        {
          int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
          notesDBs = _notesBusiness.RemainderList(UserId);
          if (notesDBs != null)
          {
            status = true;
            message = "List of all Remainder";
            return Ok(new { status, message, notesDBs });
          }
          else
          {
            status = false;
            message = "Getting List of Remainder is failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

    /// <summary>
    /// Collaborates the specified collaborate.
    /// </summary>
    /// <param name="collaborate">The collaborate.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("Collaborate")]
    public IActionResult Collaborate(MultipleCollaborate collaborate, int noteid)
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
          notesDB = _notesBusiness.Collaborate(collaborate, noteid);
          if (notesDB != null)
          {
            status = true;
            message = "collaboration successful done";
            return Ok(new { status, message, notesDB });
          }
          else
          {
            status = false;
            message = "collaboration failed";
            return NotFound(new { status, message });
          }
        }
      }
      return BadRequest("used invalid token");
    }

  }

}