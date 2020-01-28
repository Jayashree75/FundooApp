//-----------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
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

  /// <summary>
  /// This is the class for notes repository.
  /// </summary>
  public class NotesRepository : INotesRepository
  {
    private readonly UserContext _userContext;
    public NotesRepository(UserContext userContext)
    {
      _userContext = userContext;
    }

    /// <summary>
    /// This is the method for add the notes.
    /// </summary>
    /// <param name="requestedNotes"></param>
    /// <param name="userid"></param>
    /// <returns></returns>
    public async Task<NoteResponseModel> AddNotes(RequestedNotes requestedNotes, int userid)
    {
      try
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
        if (requestedNotes != null && requestedNotes.labels.Count != 0)
        {
          List<RequestNotesLabel> noteslabels = requestedNotes.labels;
          foreach (RequestNotesLabel notesLabel in noteslabels)
          {
            if (notesLabel.LabelID > 0)
            {
              var data = new Noteslabel()
              {
                LabelID = notesLabel.LabelID,
                NoteID = notesdb.NoteID
              };
              _userContext.Noteslabels.Add(data);
              _userContext.SaveChanges();

            }
          }
        }
        List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                  Join(_userContext.label,
                                                  label => label.LabelID,
                                                  note => note.LabelID,
                                                  (note, label) => new LabelResponseModel
                                                  {
                                                    LabelID = label.LabelID,
                                                    LabelName = label.LabelName,
                                                    IsCreated = label.IsCreated,
                                                    IsModified = label.IsModified
                                                  }).ToList();

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
          Image = notesdb.Color,
          labels = labelResponseModels
        };
        return noteResponseModel;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for deeletes the note.
    /// </summary>
    /// <param name="noteid"></param>
    /// <returns></returns>
    public async Task<bool> DeleteNotes(int noteid)
    {
      try
      {
        NotesDB notes = _userContext.Notes.FirstOrDefault(c => c.NoteID == noteid);
        if (notes != null)
        {
          if (notes.IsTrash == true)
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
    /// This is the method to get the list of notes.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public List<NoteResponseModel> GetNotes(int userid)
    {

      try
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


        if (notesDBs != null && notesDBs.Count != 0)
        {
          foreach (NoteResponseModel noteResponse in notesDBs)
          {
            List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                Where(notes => notes.NoteID == noteResponse.NoteID).
                                                Join(_userContext.label,
                                                label => label.LabelID,
                                                note => note.LabelID,
                                                (label, note) => new LabelResponseModel
                                                {
                                                  LabelID = label.LabelID,
                                                  LabelName = note.LabelName,
                                                  IsCreated = note.IsCreated,
                                                  IsModified = note.IsModified
                                                }).ToList();

            noteResponse.labels = labelResponseModels;
          }
        }
        return notesDBs;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is method to get notes by noteid.
    /// </summary>
    /// <param name="noteid"></param>
    /// <param name="userid"></param>
    /// <returns></returns>
    public NoteResponseModel GetNotesByNoteId(int noteid, int userid)
    {
      try
      {
        NotesDB notesDB = _userContext.Notes.Where(note => note.NoteID == noteid && note.UserId == userid).FirstOrDefault();

        List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                               Where(notes => notes.NoteID == noteid).
                                               Join(_userContext.label,
                                               label => label.LabelID,
                                               note => note.LabelID,
                                               (label, note) => new LabelResponseModel
                                               {
                                                 LabelID = label.LabelID,
                                                 LabelName = note.LabelName,
                                                 IsCreated = note.IsCreated,
                                                 IsModified = note.IsModified
                                               }).ToList();


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
            IsModified = c.IsModified,
            labels = labelResponseModels
          }).FirstOrDefault();
        return noteResponse;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method fpr updates notes.
    /// </summary>
    /// <param name="requestedNotes"></param>
    /// <param name="noteid"></param>
    /// <param name="userid"></param>
    /// <returns></returns>
    public async Task<NoteResponseModel> UpdateNotes(RequestedNotes requestedNotes, int noteid, int userid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for pin the notes.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="noteid"></param>
    /// <returns></returns>
    public async Task<bool> Pinned(int userid, int noteid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for get all pinned notes.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public List<NoteResponseModel> GetAllPinned(int userid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for trash the notes.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="noteid"></param>
    /// <returns></returns>
    public async Task<bool> Trash(int userid, int noteid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for get all trashed.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public List<NoteResponseModel> GetAllTrashed(int userid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// This is the method for to archive the pin.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="noteid"></param>
    /// <returns></returns>
    public async Task<bool> Archive(int userid, int noteid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// this is the method for to get all archive list.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>

    public List<NoteResponseModel> GetAllArchive(int userid)
    {
      try
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    ///   this is the method to get note by label id.
    /// </summary>
    /// <param name="labeid"></param>
    /// <returns></returns>
    public List<NoteResponseModel> GetNoteByLabelId(int labeid)
    {
      try
      {
        List<NoteResponseModel> noteResponseModels = _userContext.Noteslabels.Where(noteslabel => noteslabel.LabelID == labeid).
                                                    Join(_userContext.Notes,
                                                    label => label.NoteID,
                                                    note => note.NoteID,
                                                    (note, label) => new NoteResponseModel
                                                    {
                                                      NoteID = note.NoteID,
                                                      Title = label.Title,
                                                      Description = label.Description,
                                                      Color = label.Color,
                                                      Image = label.Image,
                                                      IsPin = label.IsPin,
                                                      IsArchive = label.IsArchive,
                                                      IsCreated = label.IsCreated,
                                                      IsModified = label.IsModified,
                                                      IsTrash = label.IsTrash
                                                    }).ToList();
        if (noteResponseModels.Count != 0)
        {
          return noteResponseModels;
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
    /// This is the method to delete all trash.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public bool DeleteAllTrash(int userid)
    {
      try
      {
        List<NotesDB> notes = _userContext.Notes.Where(linq => linq.UserId == userid && linq.IsTrash == true && linq.IsPin == false && linq.IsArchive == false).ToList();
        if (notes.Count != 0)
        {
          _userContext.Notes.RemoveRange(notes);
          _userContext.SaveChanges();
          return true;
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
  }
}
