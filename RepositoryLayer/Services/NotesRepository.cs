namespace FundooRepositoryLayer.Services
{
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using FundooRepositoryLayer.Interfaces;
  using FundooRepositoryLayer.ModelDB;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public class NotesRepository : INotesRepository
  {
    private readonly UserContext _userContext;
    public NotesRepository(UserContext userContext)
    {
      _userContext = userContext;
    }
    public async Task<NoteResponseModel> AddNotes(RequestedNotes requestedNotes, int userid)
    {
      NotesDB notesdb = new NotesDB()
      {
        UserId = userid,
        Title = requestedNotes.Title,
        Description = requestedNotes.Description,
        Reminder = DateTime.Now,
        IsCreated = DateTime.Now,
        IsModified = DateTime.Now,
        IsPin = requestedNotes.IsPin,
        IsArchive = requestedNotes.IsArchive
      };
      _userContext.Notes.Add(notesdb);
      await _userContext.SaveChangesAsync();
      NoteResponseModel noteResponseModel = new NoteResponseModel()
      {
        NoteID = notesdb.NoteID,
        Title = notesdb.Title,
        Description = notesdb.Description,
        Reminder = notesdb.Reminder,
        IsCreated = notesdb.IsCreated,
        IsModified = notesdb.IsModified,
        IsPin = notesdb.IsPin,
        IsArchive = notesdb.IsArchive,
        Color = notesdb.Color,
        Image = notesdb.Color
      };
      return noteResponseModel;
    }

    public async Task<bool> DeleteNotes(int noteid)
    {
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => c.NoteID == noteid);
      if (notes != null)
      {
        _userContext.Notes.Remove(notes);
        await this._userContext.SaveChangesAsync();
        return true;
      }
      else
      {
        return false;
      }
    }

    public List<NoteResponseModel> GetNotes(int userid)
    {
      List<NoteResponseModel> notesDBs = _userContext.Notes.Where(a => a.UserId == userid).
                                              Select(a => new NoteResponseModel
                                              {
                                                NoteID = a.NoteID,
                                                Title = a.Title,
                                                Description = a.Description,
                                                Reminder = a.Reminder,
                                                Image = a.Image,
                                                IsArchive = a.IsArchive,
                                                IsPin = a.IsPin,
                                                IsTrash = a.IsTrash,
                                                IsCreated = a.IsCreated,
                                                IsModified = a.IsModified
                                              }).ToList();
      return notesDBs;

    }

    public NoteResponseModel GetNotesByNoteId(int noteid, int userid)
    {

      NoteResponseModel noteResponse = _userContext.Notes.Where(c => (c.NoteID == noteid) && (c.UserId == userid)).
        Select(c => new NoteResponseModel
        {
          NoteID = c.NoteID,
          Title = c.Title,
          Description = c.Description,
          Reminder = c.Reminder,
          Image = c.Image,
          IsArchive = c.IsArchive,
          IsPin = c.IsPin,
          IsTrash = c.IsTrash,
          IsCreated = c.IsCreated,
          IsModified = c.IsModified
        }).FirstOrDefault();
      return noteResponse;
    }
    public async Task<NoteResponseModel> UpdateNotes(RequestedNotes requestedNotes, int noteid, int userid)
    {
      var notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
      NotesDB notesDB = new NotesDB();
      if (notes != null)
      {
        notes.Title = requestedNotes.Title;
        notes.Description = requestedNotes.Description;
        notes.IsModified = DateTime.Now;
        notes.Reminder = DateTime.Now;
        notes.Color = requestedNotes.Color;
        notes.Image = requestedNotes.Image;
        var note = this._userContext.Notes.Attach(notes);
        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await this._userContext.SaveChangesAsync();
      }
      NoteResponseModel responseModel = new NoteResponseModel()
      {
        NoteID = noteid,
        Title = notesDB.Title,
        Description = notesDB.Description,
        Reminder = notesDB.Reminder,
        Image = notesDB.Image,
        Color = notesDB.Color,
        IsCreated = notesDB.IsCreated,
        IsModified = notesDB.IsModified,
        IsPin = notesDB.IsPin,
        IsArchive = notesDB.IsArchive,
        IsTrash = notesDB.IsTrash
      };
      return responseModel;
    }

    public async Task<bool> Pinned(int userid, int noteid)
    {
      bool flag = false;
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
      if (notes != null)
      {
        if (notes.IsPin == false)
        {
          notes.IsPin = true;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = true;
        }
        else
        {
          notes.IsPin = false;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = false;
        }
      }
      return flag;
    }

    public List<NoteResponseModel> GetAllPinned(int userid)
    {
      List<NoteResponseModel> notesresponse = _userContext.Notes.Where(a => (a.UserId == userid) && (a.IsTrash == false) && (a.IsArchive == false) && (a.IsPin == true)).
        Select(a => new NoteResponseModel
        {
          NoteID = a.NoteID,
          Title = a.Title,
          Description = a.Description,
          Reminder = a.Reminder,
          Image = a.Image,
          IsArchive = a.IsArchive,
          IsPin = a.IsPin,
          IsTrash = a.IsTrash,
          IsCreated = a.IsCreated,
          IsModified = a.IsModified
        }).ToList();

      if (notesresponse.Count != 0)
      {
        return notesresponse;
      }
      else
      {
        return null;
      }
    }

    public async Task<bool> Trash(int userid, int noteid)
    {
      bool flag = false;
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
      if (notes != null)
      {
        if (notes.IsTrash == false)
        {
          notes.IsTrash = true;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = true;
        }
        else
        {
          notes.IsTrash = false;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = false;
        }
      }
      return flag;
    }

    public List<NoteResponseModel> GetAllTrashed(int userid)
    {
      List<NoteResponseModel> notesDBs = _userContext.Notes.Where(a => (a.UserId == userid) && (a.IsTrash == true) && (a.IsArchive == false) && (a.IsPin == false)).
        Select(a => new NoteResponseModel
        {
          NoteID = a.NoteID,
          Title = a.Title,
          Description = a.Description,
          Reminder = a.Reminder,
          Image = a.Image,
          IsArchive = a.IsArchive,
          IsPin = a.IsPin,
          IsTrash = a.IsTrash,
          IsCreated = a.IsCreated,
          IsModified = a.IsModified
        }).ToList();
      if (notesDBs.Count != 0)
      {
        return notesDBs;
      }
      else
      {
        return null;
      }
    }

    public async Task<bool> Archive(int userid, int noteid)
    {
      bool flag = false;
      NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
      if (notes != null)
      {
        if (notes.IsArchive == false)
        {
          notes.IsArchive = true;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = true;
        }
        else
        {
          notes.IsArchive = false;
          var note = this._userContext.Notes.Attach(notes);
          note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          await this._userContext.SaveChangesAsync();
          flag = false;
        }
      }
      return flag;
    }

    public List<NoteResponseModel> GetAllArchive(int userid)
    {
      List<NoteResponseModel> notesDBs = _userContext.Notes.Where(a => (a.UserId == userid) && (a.IsTrash == false) && (a.IsArchive == true) && (a.IsPin == false))
                .Select(a => new NoteResponseModel
                {
                  NoteID = a.NoteID,
                  Title = a.Title,
                  Description = a.Description,
                  Reminder = a.Reminder,
                  Image = a.Image,
                  IsArchive = a.IsArchive,
                  IsPin = a.IsPin,
                  IsTrash = a.IsTrash,
                  IsCreated = a.IsCreated,
                  IsModified = a.IsModified
                }).ToList();
      if (notesDBs.Count != 0)
      {
        return notesDBs;
      }
      else
      {
        return null;
      }
    }

  }
}
