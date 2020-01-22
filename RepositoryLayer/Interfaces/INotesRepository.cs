namespace FundooRepositoryLayer.Interfaces
{
  using FundooCommonLayer.Model;
  using System;
  using System.Collections.Generic;
  using System.Text;


  public interface INotesRepository
  {
    NotesDB AddNotes(NotesDB notes);
    List<NotesDB> GetNotes(long userid);
    NotesDB UpdateNotes(NotesDB notesDB);
    bool DeleteNotes(int noteid);
   NotesDB GetNotesByNoteId(int noteid,int userid);
    bool Trash(int userid,int noteid);
  }
}
