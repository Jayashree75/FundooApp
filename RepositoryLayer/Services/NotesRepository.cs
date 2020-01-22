namespace FundooRepositoryLayer.Services
{
  using FundooCommonLayer.Model;
  using FundooRepositoryLayer.Interfaces;
  using FundooRepositoryLayer.ModelDB;
  using System;
  using System.Collections.Generic;
  using System.Linq;


  public class NotesRepository : INotesRepository
  {
    private readonly UserContext _userContext;
    public NotesRepository(UserContext userContext)
    {
      _userContext = userContext;
    }
    public NotesDB AddNotes(NotesDB notesdb)
    {
      notesdb.IsCreated = DateTime.Now;
      notesdb.IsModified = DateTime.Now;
      _userContext.Notes.Add(notesdb);
      _userContext.SaveChanges();
      return notesdb;
    }

    public bool DeleteNotes(int noteid)
    {
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => c.NoteID == noteid);
      if (notes != null)
      {

        _userContext.Notes.Remove(notes);
        this._userContext.SaveChanges();
        return true;
      }
      else
      {
        return false;
      }
    }

    public List<NotesDB> GetNotes(long userid)
    {
      List<NotesDB> notesDBs = _userContext.Notes.Where(a => a.UserId == userid).ToList();
      if (notesDBs != null)
      {
        return notesDBs;
      }
      else
      {
        return null;
      }

    }

    public NotesDB GetNotesByNoteId(int noteid, int userid)
    {
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.NoteID == noteid) && (c.UserId == userid));
      if (notes != null)
      {
        return notes;
      }
      else
      {
        return null;
      }

    }

    public bool Trash(int userid,int noteid)
    {
      bool flag = false;
      NotesDB notes = _userContext.Notes.FirstOrDefault(c =>(c.UserId == userid)&&(c.NoteID==noteid));
      if (notes!=null)
      {
        if(notes.IsTrash==false)
        {
          notes.IsTrash = true;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          this._userContext.SaveChanges();
           flag = true;
        }
        else
        {
          notes.IsTrash = false;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          this._userContext.SaveChanges();
          flag = false;
        }
      }
      return flag;
    }

    public NotesDB UpdateNotes(NotesDB notesDB)
    {
      NotesDB notesDB1 = _userContext.Notes.FirstOrDefault(c => (c.UserId == notesDB.UserId) && (c.NoteID == notesDB.NoteID));
      if (notesDB1 != null)
      {
        notesDB1.Title = notesDB.Title;
        notesDB1.Description = notesDB.Description;
        notesDB1.IsModified = notesDB.IsModified;
        var note = this._userContext.Notes.Attach(notesDB1);
        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        this._userContext.SaveChanges();
        return notesDB1;
      }
      else
      {
        return null;
      }
    }
  }
}
