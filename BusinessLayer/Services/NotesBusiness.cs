namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
    using FundooRepositoryLayer.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;
    using System.Threading.Tasks;

    public class NotesBusiness : INotesBusiness
  {
    private readonly INotesRepository _notesRepository;
    public NotesBusiness(INotesRepository notesRepository)
    {
      _notesRepository = notesRepository;
    }
    public async Task<NoteResponseModel> AddNotes(RequestedNotes requestedNotes,int userid)
    {
      if (requestedNotes != null)
      {
        return await _notesRepository.AddNotes(requestedNotes,userid);
      }
      else
      {
        throw new Exception("Notes Detail is Empty");
      }
    }

    public async Task<bool> Archive(int userid, int noteid)
    {
      if(userid!=0 && noteid!=0)
      {
        return await _notesRepository.Archive(userid, noteid);
      }
      else
      {
        return false;
      }
    }

    public async Task<bool> DeleteNotes(int noteid)
    { 
      if(noteid!=0)
      {
        return await _notesRepository.DeleteNotes(noteid);
      }
      else
      {
        return false;
      }
    }

    public List<NoteResponseModel> GetAllArchive(int userid)
    {
      if(userid!=0)
      {
        return _notesRepository.GetAllArchive(userid);
      }
      else
      {
        return null;
      }
    }

    public List<NoteResponseModel> GetAllPinned(int userid)
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

    public List<NoteResponseModel> GetAllTrashed(int userid)
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

    public List<NoteResponseModel> GetNotes(int userid)
    {
      if(userid!=0)
      {
        return _notesRepository.GetNotes(userid);
      }
      else
      {
        return null;
      }
    }

    public NoteResponseModel GetNotesByNoteId(int noteid,int userid)
    {
      if(noteid!=0 && userid!=0)
      {
        return _notesRepository.GetNotesByNoteId(noteid,userid);
      }
      else
      {
        return null;
      }
    }

    public async Task<bool> Pinned(int userid, int noteid)
    {
     if(userid!=0 && noteid!=0)
      {
        return await _notesRepository.Pinned(userid, noteid);
      }
     else
      {
        return false;
      }
    }

    public async Task<bool> Trash(int userid, int noteid)
    {
      if(userid!=0 && noteid!=0)
      {
        return await _notesRepository.Trash(userid, noteid);
      }
      else
      {
        return false;
      }
    }

    public async Task<NoteResponseModel> UpdateNotes(RequestedNotes requestedNotes,int noteid,int userid)
    {
      if(requestedNotes != null)
      {
        return await _notesRepository.UpdateNotes(requestedNotes,noteid,userid);
      }
      else
      {
        return null;
      }
    }
  }
}
