namespace FundooBusinessLayer.Interfaces
{
    using FundooCommonLayer.Model;
    using System;
  using System.Collections.Generic;
  using System.Text;


  public interface INotesBusiness
  {
    NotesDB AddNotes(NotesDB notesDB);
    List<NotesDB> GetNotes(long userid);
    NotesDB UpdateNotes(NotesDB notesDB);
    bool DeleteNotes(int noteid);
    NotesDB GetNotesByNoteId(int noteid,int userid);
  }
}
