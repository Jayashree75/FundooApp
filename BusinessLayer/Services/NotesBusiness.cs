namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  /// <summary>
  /// This is the class NoteBusiness which implement Inotebusiness interface.
  /// </summary>
  /// <seealso cref="FundooBusinessLayer.Interfaces.INotesBusiness" />
  public class NotesBusiness : INotesBusiness
  {
    private readonly INotesRepository _notesRepository;
    public NotesBusiness(INotesRepository notesRepository)
    {
      _notesRepository = notesRepository;
    }

    public List<GetAllUserResponse> GetAllUser(string keyword)
    {
      return _notesRepository.GetAllUserList(keyword);
    }
    /// <summary>
    /// Adds the notes.
    /// </summary>
    /// <param name="requestedNotes">The requested notes.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception">
    /// Notes Detail is Empty
    /// or
    /// </exception>
    public async Task<NoteResponseModel> AddNotes(RequestedNotesUpdate requestedNotes, int userid)
    {
      try
      {
        if (requestedNotes != null)
        {
          return await _notesRepository.AddNotes(requestedNotes, userid);
        }
        else
        {
          throw new Exception("Notes Detail is Empty");
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Archives the specified noteid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> Archive(int userid, int noteid,TrashValue archive)
    {
      try
      {
        if (userid != 0 && noteid != 0)
        {
          return await _notesRepository.Archive(userid, noteid,archive);
        }
        else
        {
          return false;
        }
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
    public async Task<bool> DeleteNotes(int noteid,int userid)
    {
      try
      {
        if (noteid != 0)
        {
          return await _notesRepository.DeleteNotes(noteid,userid);
        }
        else
        {
          return false;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets all archive.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NoteResponseModel> GetAllArchive(int userid)
    {
      try
      {
        if (userid != 0)
        {
          return _notesRepository.GetAllArchive(userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets all pinned.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NoteResponseModel> GetAllPinned(int userid)
    {
      try
      {
        if (userid != 0)
        {
          return _notesRepository.GetAllPinned(userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets all trashed.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NoteResponseModel> GetAllTrashed(int userid)
    {
      try
      {
        if (userid != 0)
        {
          return _notesRepository.GetAllTrashed(userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the note by label identifier.
    /// </summary>
    /// <param name="labeid">The labeid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NoteResponseModel> GetNoteByLabelId(int labeid,int userid)
    {
      try
      {
        if (labeid != 0)
        {
          return _notesRepository.GetNoteByLabelId(labeid,userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the notes.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NoteResponseModel> GetNotes(int userid,string keyword)
    {
      try
      {
        if (userid != 0)
        {
          return _notesRepository.GetNotes(userid,keyword);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Gets the notes by note identifier.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public NoteResponseModel GetNotesByNoteId(int noteid, int userid)
    {
      try
      {
        if (noteid != 0 && userid != 0)
        {
          return _notesRepository.GetNotesByNoteId(noteid, userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Pin the specified noteid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> Pinned(int userid, int noteid,TrashValue pin)
    {
      try
      {
        if (userid != 0 && noteid != 0)
        {
          return await _notesRepository.Pinned(userid, noteid,pin);
        }
        else
        {
          return false;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Trashes the specified noteid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> Trash(int userid, int noteid,TrashValue trash)
    {
      try
      {
        if (userid != 0 && noteid != 0)
        {
          return await _notesRepository.Trash(userid, noteid,trash);
        }
        else
        {
          return false;
        }
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
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<NoteResponseModel> UpdateNotes(RequestNotes requestedNotes, int noteid, int userid)
    {
      try
      {
        if (requestedNotes != null)
        {
          return await _notesRepository.UpdateNotes(requestedNotes, noteid, userid);
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Deletes all trash.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool DeleteAllTrash(int userid)
    {
      try
      {
        if (userid != 0)
        {
          return this._notesRepository.DeleteAllTrash(userid);
        }
        else
        {
          return false;
        }
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
    /// <summary>
    /// Colors the change.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="requestColour">The request colour.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    public NoteResponseModel ColorChange(int noteid, RequestColour requestColour, int userid)
    {
      if (requestColour != null)
      {
        return _notesRepository.ColorChange(noteid, requestColour, userid);
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Add the image.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="userid">The userid.</param>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public string AddImage(int noteid, int userid, ImageUpload image)
    {
      if (image != null)
      {
        return _notesRepository.AddImage(noteid, userid, image);
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// get the Remainder list.
    /// </summary>
    /// <param name="Userid">The userid.</param>
    /// <returns></returns>
    public List<NoteResponseModel> RemainderList(int Userid)
    {
      if (Userid != 0)
      {
        return _notesRepository.RemainderList(Userid);
      }
      else
      {
        return null;
      }
    }

    public NoteResponseModel Collaborate(int noteid,MultipleCollaborate collaborate)
    {
      if(noteid!=0 && collaborate!=null)
      {
        return _notesRepository.Collaborate(noteid,collaborate);
      }
      else
      {
        return null;
      }
    }

  }
}
