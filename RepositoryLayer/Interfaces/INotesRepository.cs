//-----------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
{
  using FundooCommonLayer.Model;
  using FundooCommonLayer.ModelRequest;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Threading.Tasks;

  /// <summary>
  /// This is the interface for note Repository.
  /// </summary>
  public interface INotesRepository
  {
    /// <summary>
    /// Adds the notes.
    /// </summary>
    /// <param name="requestedNotes">The requested notes.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    Task<NoteResponseModel> AddNotes(RequestedNotes requestedNotes, int userid);

    /// <summary>
    /// Gets the notes.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    List<NoteResponseModel> GetNotes(int userid);

    /// <summary>
    /// Updates the notes.
    /// </summary>
    /// <param name="requestedNotes">The requested notes.</param>
    /// <param name="noteid">The noteid.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    Task<NoteResponseModel> UpdateNotes(RequestedNotes requestedNotes, int noteid, int userid);

    /// <summary>
    /// Deletes the notes.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    Task<bool> DeleteNotes(int noteid);

    /// <summary>
    /// Gets the notes by note identifier.
    /// </summary>
    /// <param name="noteid">The noteid.</param>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    NoteResponseModel GetNotesByNoteId(int noteid, int userid);

    /// <summary>
    /// Trashes the specified userid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    Task<bool> Trash(int userid, int noteid);

    /// <summary>
    /// Pinneds the specified userid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    Task<bool> Pinned(int userid, int noteid);

    /// <summary>
    /// Archives the specified userid.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <param name="noteid">The noteid.</param>
    /// <returns></returns>
    Task<bool> Archive(int userid, int noteid);

    /// <summary>
    /// Gets all pinned.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    List<NoteResponseModel> GetAllPinned(int userid);

    /// <summary>
    /// Gets all trashed.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    List<NoteResponseModel> GetAllTrashed(int userid);

    /// <summary>
    /// Gets all archive.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    List<NoteResponseModel> GetAllArchive(int userid);

    /// <summary>
    /// Gets the note by label identifier.
    /// </summary>
    /// <param name="labeid">The labeid.</param>
    /// <returns></returns>
    List<NoteResponseModel> GetNoteByLabelId(int labeid);

    /// <summary>
    /// Deletes all trash.
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <returns></returns>
    bool DeleteAllTrash(int userid);
  }
}
