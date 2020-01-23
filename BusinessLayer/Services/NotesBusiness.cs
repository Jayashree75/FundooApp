namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
    using FundooRepositoryLayer.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class NotesBusiness : INotesBusiness
  {
    private readonly INotesRepository _notesRepository;
    public NotesBusiness(INotesRepository notesRepository)
    {
      _notesRepository = notesRepository;
    }
    public NotesDB AddNotes(RequestedNotes requestedNotes,int userid)
    {
      if (requestedNotes != null)
      {
        return _notesRepository.AddNotes(requestedNotes,userid);
      }
      else
      {
        throw new Exception("Notes Detail is Empty");
      }
    }

    public bool Archive(int userid, int noteid)
    {
      if(userid!=0 && noteid!=0)
      {
        return _notesRepository.Archive(userid, noteid);
      }
      else
      {
        return false;
      }
    }

    public bool DeleteNotes(int noteid)
    { 
      if(noteid!=0)
      {
        return _notesRepository.DeleteNotes(noteid);
      }
      else
      {
        return false;
      }
    }

    public List<NotesDB> GetAllArchive(int userid)
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

    public List<NotesDB> GetAllPinned(int userid)
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

    public List<NotesDB> GetAllTrashed(int userid)
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

    public List<NotesDB> GetNotes(int userid)
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

    public NotesDB GetNotesByNoteId(int noteid,int userid)
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

    public bool Pinned(int userid, int noteid)
    {
     if(userid!=0 && noteid!=0)
      {
        return _notesRepository.Pinned(userid, noteid);
      }
     else
      {
        return false;
      }
    }

    public bool Trash(int userid, int noteid)
    {
      if(userid!=0 && noteid!=0)
      {
        return _notesRepository.Trash(userid, noteid);
      }
      else
      {
        return false;
      }
    }

    public NotesDB UpdateNotes(NotesDB notesDB)
    {
      if(notesDB!=null)
      {
        return _notesRepository.UpdateNotes(notesDB);
      }
      else
      {
        return null;
      }
    }
  }
}
