namespace FundooBusinessLayer.Interfaces
{
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Threading.Tasks;

  public interface INotesBusiness
  {
    Task<NoteResponseModel> AddNotes(RequestedNotes requestedNotes, int userid);
    List<NoteResponseModel> GetNotes(int userid);
    Task<NoteResponseModel> UpdateNotes(RequestedNotes requestedNotes, int noteid,int userid);
    Task<bool> DeleteNotes(int noteid);
    NoteResponseModel GetNotesByNoteId(int noteid, int userid);
    Task<bool> Trash(int userid, int noteid);
    Task<bool> Archive(int userid, int noteid);
    Task<bool> Pinned(int userid, int noteid);
    List<NoteResponseModel> GetAllPinned(int userid);
    List<NoteResponseModel> GetAllTrashed(int userid);
    List<NoteResponseModel> GetAllArchive(int userid);
  }
}