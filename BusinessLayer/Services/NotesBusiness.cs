namespace FundooBusinessLayer.Services
{
  using FundooBusinessLayer.Interfaces;
  using FundooCommonLayer.Model;
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
    public NotesDB AddNotes(NotesDB notesDB)
    {
      if (notesDB != null)
      {
        return _notesRepository.AddNotes(notesDB);
      }
      else
      {
        throw new Exception("Notes Detail is Empty");
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

    public List<NotesDB> GetNotes(long userid)
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
