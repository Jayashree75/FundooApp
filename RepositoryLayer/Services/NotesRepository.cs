//-----------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Services
{
  using CommonLayer.Model;
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
    public async Task<NoteResponseModel> AddNotes(RequestedNotesUpdate requestedNotes, int userid)
    {
      try
      {
        NotesDB notesdb = new NotesDB()
        {
          UserId = userid,
          Title = requestedNotes.Title,
          Description = requestedNotes.Description,
          Reminder = DateTime.Now,
          Image = string.IsNullOrWhiteSpace(requestedNotes.Image) ? "null" : ImageModel.ImageAdd(requestedNotes.Image),
          Color = string.IsNullOrWhiteSpace(requestedNotes.Color) ? "null" : requestedNotes.Color,
          IsCreated = DateTime.Now,
          IsModified = DateTime.Now,
          IsPin = requestedNotes.IsPin,
          IsTrash = requestedNotes.IsTrash,
          IsArchive = requestedNotes.IsArchive
        };
        _userContext.Notes.Add(notesdb);
        await _userContext.SaveChangesAsync();
        if (requestedNotes != null && requestedNotes.labels.Count != 0)
        {
          List<RequestNotesLabel> noteslabels = requestedNotes.labels;
          foreach (RequestNotesLabel notesLabel in noteslabels)
          {
            LabelModel labelModel = _userContext.label.FirstOrDefault(linq => linq.UserId == userid && linq.LabelID == notesLabel.LabelID);
            if (notesLabel.LabelID > 0 && labelModel != null)
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
        List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.Where(note => note.NoteID == notesdb.NoteID).
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

        if (requestedNotes != null && requestedNotes.collaborateRequests.Count != 0)
        {
          List<CollaborateRequest> collaborateRequests = requestedNotes.collaborateRequests;
          foreach (CollaborateRequest collaborate in collaborateRequests)
          {
            var data = new CollaborateDb()
            {
              UserId = collaborate.UserId,
              NoteID = notesdb.NoteID
            };
            _userContext.collaborates.Add(data);
            _userContext.SaveChanges();
          }
        }
        List<CollaborateResponse> collaborateResponses = _userContext.collaborates.Where(note => note.NoteID == notesdb.NoteID).Join(_userContext.Users,
          user => user.UserId,
          note => note.UserId,
          (user, note) => new CollaborateResponse
          {
            UserId = user.UserId,
            FirstName = note.FirstName,
            LastName = note.LastName,
            Email = note.Email
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
          IsTrash = notesdb.IsTrash,
          IsArchive = notesdb.IsArchive,
          Color = notesdb.Color,
          Image = notesdb.Image,
          labels = labelResponseModels,
          CollaborateResponse = collaborateResponses
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
        NotesDB notes = _userContext.Notes.FirstOrDefault(note => note.NoteID == noteid);
        if (notes != null)
        {
          List<Noteslabel> noteslabels = _userContext.Noteslabels.Where(linq => linq.NoteID == noteid).ToList();
          _userContext.Noteslabels.RemoveRange(noteslabels);
          _userContext.SaveChanges();
          List<CollaborateDb> collaborateDbs = _userContext.collaborates.Where(linq => linq.NoteID == noteid).ToList();
          _userContext.collaborates.RemoveRange(collaborateDbs);
          _userContext.SaveChanges();
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
    public List<NoteResponseModel> GetNotes(int userid, string keyword)
    {
      try
      {
        List<NoteResponseModel> notesDBs = _userContext.Notes.Where(note => note.UserId == userid).
                                                 Select(note => new NoteResponseModel
                                                 {
                                                   NoteID = note.NoteID,
                                                   Title = note.Title,
                                                   Description = note.Description,
                                                   Reminder = note.Reminder,
                                                   Image = note.Image,
                                                   IsArchive = note.IsArchive,
                                                   IsPin = note.IsPin,
                                                   IsTrash = note.IsTrash,
                                                   IsCreated = note.IsCreated,
                                                   IsModified = note.IsModified
                                                 }).ToList();
        if (notesDBs != null && notesDBs.Count != 0)
        {
          foreach (NoteResponseModel noteResponse in notesDBs)
          {
            List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                Where(notes => notes.NoteID == noteResponse.NoteID).
                                                Join(_userContext.label,
                                                noteslabel => noteslabel.LabelID,
                                                label => label.LabelID,
                                                (noteslabel, label) => new LabelResponseModel
                                                {
                                                  LabelID = noteslabel.LabelID,
                                                  LabelName = label.LabelName,
                                                  IsCreated = label.IsCreated,
                                                  IsModified = label.IsModified
                                                }).ToList();
            noteResponse.labels = labelResponseModels;

            List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
              collab => collab.UserId,
              user => user.UserId,
              (collab, user) => new CollaborateResponse
              {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
              }).ToList();
            noteResponse.CollaborateResponse = collaborates;
          }
        }
        List<NoteResponseModel> noteResponseModels1 = _userContext.collaborates.Where(linq => linq.UserId == userid).Join(_userContext.Notes,
          collab => collab.NoteID,
          note => note.NoteID,
          (collab, note) => new NoteResponseModel
          {
            Title = note.Title,
            Description = note.Description,
            Reminder = note.Reminder,
            Image = note.Image,
            IsArchive = note.IsArchive,
            IsPin = note.IsPin,
            IsTrash = note.IsTrash,
            IsCreated = note.IsCreated,
            IsModified = note.IsModified,
            NoteID = note.NoteID
          }).ToList();
        foreach (var collab in noteResponseModels1)
        {
          List<CollaborateResponse> collaborateResponses = _userContext.Notes.
            Where(note => note.NoteID == collab.NoteID)
            .Join(_userContext.Users,
            collaborate => collaborate.UserId,
            note => note.UserId, (collaborate, note) => new CollaborateResponse
            {
              UserId = note.UserId,
              Email = note.Email,
              FirstName = note.FirstName,
              LastName = note.LastName
            }).ToList();
          collab.CollaborateResponse = collaborateResponses;
        }
        if (keyword != null)
        {
          List<NoteResponseModel> noteResponseModels = SearchNote(userid, keyword);
          return noteResponseModels;
        }
        else
        {
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
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    /// <summary>
    /// Searches the note.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="keyword">The keyword.</param>
    /// <returns></returns>
    public List<NoteResponseModel> SearchNote(int userid, string keyword)
    {
      List<NoteResponseModel> noteResponseModels = null;
      if (keyword != null)
      {
        noteResponseModels = _userContext.Notes.Where(linq => (linq.Title.Contains(keyword) || linq.Description.Contains(keyword)) && (linq.UserId == userid)).
          Select(linq => new NoteResponseModel
          {
            NoteID = linq.NoteID,
            Title = linq.Title,
            Description = linq.Description,
            Reminder = linq.Reminder,
            Image = linq.Image,
            IsArchive = linq.IsArchive,
            IsTrash = linq.IsTrash,
            IsPin = linq.IsPin,
            IsCreated = linq.IsCreated,
            IsModified = linq.IsModified
          }
        ).ToList();
      }
      return noteResponseModels;

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

        List<CollaborateResponse> collaborateResponses = _userContext.collaborates.Where(note => note.NoteID == notesDB.NoteID).Join(_userContext.Users,
          user => user.UserId,
          note => note.UserId,
          (user, note) => new CollaborateResponse
          {
            UserId = user.UserId,
            FirstName = note.FirstName,
            LastName = note.LastName,
            Email = note.Email
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
            labels = labelResponseModels,
            CollaborateResponse = collaborateResponses
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
    public async Task<NoteResponseModel> UpdateNotes(RequestNotes request, int noteid, int userid)
    {
      try
      {
        var notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
        NotesDB notesDB = new NotesDB();
        if (notes != null)
        {
          notes.Title = request.Title;
          notes.Description = request.Description;
          notes.IsModified = DateTime.Now;
          notes.Reminder = DateTime.Now;
          notes.Color = string.IsNullOrWhiteSpace(request.Color) ? "null" : request.Color;
          notes.Image = string.IsNullOrWhiteSpace(request.Image) ? "null" : ImageModel.ImageAdd(request.Image);
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
    public async Task<bool> Pinned(int userid, int noteid, TrashValue pin)
    {
      try
      {
        bool flag = pin.Value;
        NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
        if (notes != null)
        {
          if (flag)
          {
            notes.IsPin = true;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return true;
          }
          else
          {
            notes.IsPin = false;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return false;
          }
        }
        return false;
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
        List<NoteResponseModel> notesDBs = _userContext.Notes.Where(a => (a.UserId == userid) && (a.IsTrash == false) && (a.IsArchive == false) && (a.IsPin == true)).
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
                                                noteslabel => noteslabel.LabelID,
                                                label => label.LabelID,
                                                (noteslabel, label) => new LabelResponseModel
                                                {
                                                  LabelID = noteslabel.LabelID,
                                                  LabelName = label.LabelName,
                                                  IsCreated = label.IsCreated,
                                                  IsModified = label.IsModified
                                                }).ToList();
            noteResponse.labels = labelResponseModels;

            List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
             collab => collab.UserId,
             user => user.UserId,
             (collab, user) => new CollaborateResponse
             {
               UserId = user.UserId,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName
             }).ToList();
            noteResponse.CollaborateResponse = collaborates;
          }
        }
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
    /// This is the method for trash the notes.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="noteid"></param>
    /// <returns></returns>
    public async Task<bool> Trash(int userid, int noteid, TrashValue trash)
    {
      try
      {
        bool flag = trash.Value;
        NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
        if (notes != null)
        {
          if (notes.IsTrash == false)
          {
            notes.IsTrash = true;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return true;
          }
          else
          {
            notes.IsTrash = false;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return false;
          }
        }
        return false;
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
        if (notesDBs != null && notesDBs.Count != 0)
        {
          foreach (NoteResponseModel noteResponse in notesDBs)
          {
            List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                Where(notes => notes.NoteID == noteResponse.NoteID).
                                                Join(_userContext.label,
                                                noteslabel => noteslabel.LabelID,
                                                label => label.LabelID,
                                                (noteslabel, label) => new LabelResponseModel
                                                {
                                                  LabelID = noteslabel.LabelID,
                                                  LabelName = label.LabelName,
                                                  IsCreated = label.IsCreated,
                                                  IsModified = label.IsModified
                                                }).ToList();
            noteResponse.labels = labelResponseModels;
            List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
             collab => collab.UserId,
             user => user.UserId,
             (collab, user) => new CollaborateResponse
             {
               UserId = user.UserId,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName
             }).ToList();
            noteResponse.CollaborateResponse = collaborates;
          }
        }
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
    public async Task<bool> Archive(int userid, int noteid, TrashValue archive)
    {
      try
      {
        bool flag = archive.Value;
        NotesDB notes = _userContext.Notes.FirstOrDefault(c => (c.UserId == userid) && (c.NoteID == noteid));
        if (notes != null)
        {
          if (notes.IsArchive == false)
          {
            notes.IsArchive = true;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return true;
          }
          else
          {
            notes.IsArchive = false;
            var note = this._userContext.Notes.Attach(notes);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await this._userContext.SaveChangesAsync();
            return false;
          }
        }
        return false;
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
        if (notesDBs != null && notesDBs.Count != 0)
        {
          foreach (NoteResponseModel noteResponse in notesDBs)
          {
            List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                Where(notes => notes.NoteID == noteResponse.NoteID).
                                                Join(_userContext.label,
                                                noteslabel => noteslabel.LabelID,
                                                label => label.LabelID,
                                                (noteslabel, label) => new LabelResponseModel
                                                {
                                                  LabelID = noteslabel.LabelID,
                                                  LabelName = label.LabelName,
                                                  IsCreated = label.IsCreated,
                                                  IsModified = label.IsModified
                                                }).ToList();
            noteResponse.labels = labelResponseModels;
            List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
             collab => collab.UserId,
             user => user.UserId,
             (collab, user) => new CollaborateResponse
             {
               UserId = user.UserId,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName
             }).ToList();
            noteResponse.CollaborateResponse = collaborates;
          }
        }
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
        if (noteResponseModels != null && noteResponseModels.Count != 0)
        {
          foreach (NoteResponseModel noteResponse in noteResponseModels)
          {
            List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                                Where(notes => notes.NoteID == noteResponse.NoteID).
                                                Join(_userContext.label,
                                                noteslabel => noteslabel.LabelID,
                                                label => label.LabelID,
                                                (noteslabel, label) => new LabelResponseModel
                                                {
                                                  LabelID = noteslabel.LabelID,
                                                  LabelName = label.LabelName,
                                                  IsCreated = label.IsCreated,
                                                  IsModified = label.IsModified
                                                }).ToList();
            noteResponse.labels = labelResponseModels;
            List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
             collab => collab.UserId,
             user => user.UserId,
             (collab, user) => new CollaborateResponse
             {
               UserId = user.UserId,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName
             }).ToList();
            noteResponse.CollaborateResponse = collaborates;
          }
        }
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
          foreach (NotesDB data in notes)
          {
            List<Noteslabel> noteslabels = _userContext.Noteslabels.Where(linq => linq.NoteID == data.NoteID).ToList();
            _userContext.Noteslabels.RemoveRange(noteslabels);
            _userContext.SaveChanges();
            List<CollaborateDb> collaborateDbs = _userContext.collaborates.Where(linq => linq.NoteID == data.NoteID).ToList();
            _userContext.collaborates.RemoveRange(collaborateDbs);
            _userContext.SaveChanges();
          }
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

    /// <summary>
    /// method for change color.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="requestColour">The request colour.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    public NoteResponseModel ColorChange(int noteid, RequestColour requestColour, int userid)
    {
      var notes = _userContext.Notes.FirstOrDefault(linq => linq.NoteID == noteid && linq.UserId == userid);
      if (notes != null)
      {
        notes.Color = requestColour.Color;
        var note = this._userContext.Notes.Attach(notes);
        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        this._userContext.SaveChanges();
      }
      NoteResponseModel noteResponseModel = _userContext.Notes.Where(linq => linq.NoteID == noteid && linq.UserId == userid).Select(linq => new NoteResponseModel
      {
        Color = requestColour.Color
      }).FirstOrDefault();
      return noteResponseModel;
    }

    /// <summary>
    /// Add the image.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="userid">The userid.</param>
    /// <param name="imageModel">The image model.</param>
    /// <returns></returns>
    public string AddImage(int noteid, int userid, ImageUpload imageModel)
    {
      var data = _userContext.Notes.FirstOrDefault(linq => (linq.UserId == userid) && (linq.NoteID == noteid));
      if (data != null)
      {
        string imageurl = ImageModel.ImageAdd(imageModel.Image);
        data.Image = imageurl;
        var note = this._userContext.Notes.Attach(data);
        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        this._userContext.SaveChanges();
        return imageurl;
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
      List<NoteResponseModel> noteResponseModels = _userContext.Notes.Where(linq => linq.Reminder != null && linq.UserId == Userid).Select(linq => new NoteResponseModel
      {
        NoteID = linq.NoteID,
        Title = linq.Title,
        Description = linq.Description,
        Reminder = linq.Reminder,
        Image = linq.Image,
        IsArchive = linq.IsArchive,
        IsPin = linq.IsPin,
        IsTrash = linq.IsTrash,
        IsCreated = linq.IsCreated,
        IsModified = linq.IsModified
      }).ToList();
      if (noteResponseModels != null && noteResponseModels.Count != 0)
      {
        foreach (NoteResponseModel noteResponse in noteResponseModels)
        {
          List<LabelResponseModel> labelResponseModels = _userContext.Noteslabels.
                                              Where(notes => notes.NoteID == noteResponse.NoteID).
                                              Join(_userContext.label,
                                              noteslabel => noteslabel.LabelID,
                                              label => label.LabelID,
                                              (noteslabel, label) => new LabelResponseModel
                                              {
                                                LabelID = noteslabel.LabelID,
                                                LabelName = label.LabelName,
                                                IsCreated = label.IsCreated,
                                                IsModified = label.IsModified
                                              }).ToList();
          noteResponse.labels = labelResponseModels;
          List<CollaborateResponse> collaborates = _userContext.collaborates.Where(note => note.NoteID == noteResponse.NoteID).Join(_userContext.Users,
             collab => collab.UserId,
             user => user.UserId,
             (collab, user) => new CollaborateResponse
             {
               UserId = user.UserId,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName
             }).ToList();
          noteResponse.CollaborateResponse = collaborates;
        }
      }
      noteResponseModels.Sort((note1, note2) => DateTime.Now.CompareTo(note1.Reminder.Value));

      if (noteResponseModels.Count != 0)
      {
        return noteResponseModels;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Collaborates the specified userid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    public NoteResponseModel Collaborate(MultipleCollaborate collaborate, int noteid)
    {
      var notesdb = _userContext.Notes.FirstOrDefault(linq => linq.NoteID == noteid);
      if (notesdb != null && collaborate.Collaborates.Count != 0)
      {
        foreach (CollaborateRequest request in collaborate.Collaborates)
        {
          UserDetails user = _userContext.Users.FirstOrDefault(linq => linq.UserId == request.UserId);
          if (request.UserId != 0 && user != null)
          {
            var data = new CollaborateDb()
            {
              NoteID = notesdb.NoteID,
              UserId = user.UserId
            };
            _userContext.collaborates.Add(data);
            _userContext.SaveChanges();
          }
        }
      }
      List<CollaborateResponse> collaborateRequests = _userContext.collaborates.Where(note => note.NoteID == notesdb.NoteID).
                                                Join(_userContext.Users,
                                                user => user.UserId,
                                                collab => collab.UserId,
                                                (user, collab) => new CollaborateResponse
                                                {
                                                  UserId = user.UserId,
                                                  FirstName = collab.FirstName,
                                                  LastName = collab.LastName,
                                                  Email = collab.Email
                                                }).ToList();

      NoteResponseModel noteResponse = new NoteResponseModel()
      {
        NoteID = notesdb.NoteID,
        Title = notesdb.Title,
        Description = notesdb.Description,
        IsCreated = notesdb.IsCreated,
        IsModified = notesdb.IsModified,
        Reminder = notesdb.Reminder,
        Image = notesdb.Image,
        IsArchive = notesdb.IsArchive,
        Color = notesdb.Color,
        IsPin = notesdb.IsPin,
        CollaborateResponse = collaborateRequests
      };
      return noteResponse;
    }
  }
}
