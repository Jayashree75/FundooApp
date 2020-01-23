namespace FundooRepositoryLayer.Interfaces
{
  using FundooCommonLayer.Model;
    using FundooCommonLayer.ModelRequest;
    using System;
  using System.Collections.Generic;
  using System.Text;


  public interface INotesRepository
  {
    NotesDB AddNotes(RequestedNotes requestedNotes,int userid);
    List<NotesDB> GetNotes(int userid);
    NotesDB UpdateNotes(NotesDB notesDB);
    bool DeleteNotes(int noteid);
    NotesDB GetNotesByNoteId(int noteid, int userid);
    bool Trash(int userid, int noteid);
    bool Pinned(int userid, int noteid);
    bool Archive(int userid, int noteid);
    List<NotesDB> GetAllPinned(int userid);
    List<NotesDB> GetAllTrashed(int userid);
    List<NotesDB> GetAllArchive(int userid);
  }
}
